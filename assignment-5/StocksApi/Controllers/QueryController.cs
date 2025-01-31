using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StocksApi.Dtos;
using StocksApi.BAL.Interfaces;
using StocksApi.DAL.Entities;


namespace StocksApi.Controllers
{   
    [ApiController]
    [Route("/api/stocks")]
    public class QueryController : ControllerBase
    {   
        
        private readonly IStocksServices _stocksService;
        private readonly IMapper _mapper;

        public QueryController(IStocksServices stockService, IMapper mapper){
            _stocksService = stockService;
            _mapper = mapper;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAll(){
            var stocks = await _stocksService.GetAllStocksAsync();

            if (stocks == null || !stocks.Any())
            {
                return NotFound("No stocks found.");
            }

            var StocksDto = _mapper.Map<IEnumerable<StocksDto>>(stocks); // Mapping from stocks Entity to StocksDto

            foreach(var stock in StocksDto){
                stock.IsValueForMoney = _stocksService.IsValueForMoney(stock.Price, stock.Kms);
            }

            return Ok(StocksDto);
        }

        // Filters Get By Filter - FuelType, Budget 
        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] FilterDto filter){
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var filters = _mapper.Map<FilterDto,Filters>(filter); // Mapping from FilterDto to Filters

            var stocks = await _stocksService.GetStocksByFilterAsync(filters);

            if (stocks == null || !stocks.Any())
            {
                return NotFound("No stocks found matching the specified criteria.");
            }

            var StocksDto = _mapper.Map<IEnumerable<StocksDto>>(stocks); // Mapping from stocks Entity to StocksDto


            foreach(var stock in StocksDto){
                stock.IsValueForMoney = _stocksService.IsValueForMoney(stock.Price, stock.Kms);
            }

            return Ok(StocksDto);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStocksDto createStocksDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                var stock = _mapper.Map<StocksCars>(createStocksDto); // Map DTO to entity
                var createdStock = await _stocksService.CreateStockAsync(stock);

                if (createdStock == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the stock.");
                }
                var stockDto = _mapper.Map<StocksDto>(createdStock); // Map entity back to DTO
                return Ok(stockDto);
            }
            catch (Exception ex)
            {   
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while processing your request.");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStocksDto updateStocksDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = _mapper.Map<StocksCars>(updateStocksDto); // Map DTO to entity
            var isUpdated = await _stocksService.UpdateStockAsync(stock);

            if (!isUpdated)
                return NotFound("Stock not found or could not be updated.");

            return Ok("Stock updated successfully.");            
        }
        

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var isDeleted = await _stocksService.DeleteStockAsync(id);

            if (!isDeleted)
                return NotFound("Stock not found or could not be deleted.");

            return Ok("Stock deleted successfully.");
        }

    }
}