using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Carwale.Dtos;
using Carwale.DAL.Entities;
using Carwale.GrpcServices;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;


namespace Carwale.Controllers
{   
    [ApiController]
    [Route("/api/stocks")]
    public class QueryController : ControllerBase
    {   
        private readonly StocksServ.StocksServClient _stocksClient;
        private readonly IMapper _mapper;

        public QueryController(StocksServ.StocksServClient stocksClient, IMapper mapper){
            _stocksClient = stocksClient;
            _mapper = mapper;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10){
            
            var request = new StocksRequest
            {
                Pagination = new PaginationRequest
                {
                    PageNo = pageNo,
                    PageSize = pageSize
                }
            };
            
            var response = await _stocksClient.GetAllStocksAsync(request);

            if (response.Stocks == null )
            {
                return NotFound("No stocks found.");
            }            

            try
            {
                var stocksDto = _mapper.Map<IEnumerable<StocksDto>>(response.Stocks); // Mapping from stocks response to StocksDto
                return Ok(stocksDto);
            }
            catch (System.Exception)
            {   
                // Console.WriteLine(response.Stocks);
                Console.WriteLine("Error in mapping from stocks to StocksDto");
                throw;
            }
        }

        [HttpGet("all-cities")]
        public async Task<IActionResult> GetAllCities(){
            var response = await _stocksClient.GetAllCitiesAsync(new Empty());

            if (response.Cities == null )
            {
                return NotFound("No cities found.");
            }   

            try
            {
                var citiesDto = _mapper.Map<CitiesDto>(response.Cities); 
                return Ok(citiesDto);
            }
            catch (System.Exception)
            {   
                Console.WriteLine("Error in mapping from stocks to StocksDto");
                throw;
            }
        }

        [HttpGet("all-makes")]
        public async Task<IActionResult> GetAllMakeNames()
        {
            try
            {
                var response = await _stocksClient.GetAllMakeNamesAsync(new Empty());

                if (response?.Makes == null || response.Makes.Count == 0)
                {
                    return NotFound("No make names found.");
                }

                var makeNamesDto = _mapper.Map<IEnumerable<MakeNameDto>>(response.Makes);
                return Ok(makeNamesDto);
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"gRPC Error: {ex.Status.Detail}");
                return StatusCode(500, $"gRPC service error: {ex.Status.Detail}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching make names.");
            }
        }

        [HttpGet("models-by-makeId/{makeId}")]
        public async Task<IActionResult> GetAllModelsByMakeId(int makeId)
        {
            try
            {
                var response = await _stocksClient.GetAllModelsByMakeIdAsync(new MakeRequest { MakeId = makeId });

                if (response?.Models == null || response.Models.Count == 0)
                {
                    return NotFound($"No models found for make ID {makeId}.");
                }

                // Console.WriteLine("😎😎😉  Response of Models by MakeId : "+response.Models);

                var modelsDto = _mapper.Map<IEnumerable<ModelDto>>(response.Models);
                return Ok(modelsDto);
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"gRPC Error: {ex.Status.Detail}");
                return StatusCode(500, $"gRPC service error: {ex.Status.Detail}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching models.");
            }
        }




        // Filters Get By Filter - FuelType, Budget 
        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter([FromQuery] FilterDto filter, [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10){
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Create StocksRequest directly
                var stocksRequest = new StocksRequest
                {
                    Filter = new Filter
                    {
                        Budget = filter.Budget ?? "",
                        FuelTypes = filter.FuelTypes ?? "",
                        CityId = filter.CityId ?? 0
                    },
                    Pagination = new PaginationRequest
                    {
                        PageNo = pageNo,
                        PageSize = pageSize
                    }
                };

                // Console.WriteLine($"Sending gRPC request - Budget: {stocksRequest.Filter.Budget}, FuelType: {stocksRequest.Filter.FuelTypes}");

                var response = await _stocksClient.GetStocksByFilterAsync(stocksRequest);

                if (response?.Stocks == null || response.Stocks.Count == 0)
                {
                    return NotFound("No stocks found matching the specified criteria.");
                }

                var stocksDto = _mapper.Map<IEnumerable<StocksDto>>(response.Stocks);
                return Ok(stocksDto);
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"gRPC Error: {ex.Status.Detail}");
                Console.WriteLine($"Status code: {ex.Status.StatusCode}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return StatusCode(500, $"gRPC service error: {ex.Status.Detail}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStocksDto createStocksDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {   
                if (string.IsNullOrEmpty(createStocksDto.Image))
                {
                    createStocksDto.Image = "https://images.pexels.com/photos/1670457/pexels-photo-1670457.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2"; 
                }

                // Map CreateStocksDto to StocksCars
                var stock = _mapper.Map<StocksCars>(createStocksDto); // Map DTO to entity

                // Map StocksCars to StockCreationRequest for the gRPC call
                var stockCreationRequest = _mapper.Map<StockCreationRequest>(stock);

                // For creating stock, you need a method in your gRPC service that handles creation
                var response = await _stocksClient.CreateStockAsync(stockCreationRequest);

                if (response == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the stock.");
                }
                var stockDto = _mapper.Map<StocksDto>(response); // Map entity back to DTO
                return Ok(stockDto);
            }
            catch (Exception ex)
            {   
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while processing your request.");
            }
        }
        

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var response = await _stocksClient.DeleteStockAsync(new StockDeleteRequest { Id = id });

            if (response == null)
                return NotFound("Stock not found or could not be deleted.");

            return Ok("Stock deleted successfully.");
        }

    }
}