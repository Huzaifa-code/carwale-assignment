using Carwale.DAL.Entities;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using Carwale.GrpcServices;
using AutoMapper;
using Carwale.DAL.Interfaces;

namespace Carwale.Services.Services
{
    public class StocksService : StocksServ.StocksServBase
    {
        private readonly IStocksRepo _stockRepo;
        private readonly IMapper _mapper;

        public StocksService(IStocksRepo stockRepo,IMapper mapper)
        {   
            _stockRepo = stockRepo;
            _mapper = mapper;
        }

       
        public override async Task<StocksResponse> GetAllStocks(StocksRequest request, ServerCallContext context)
        {
            int pageNo = request.Pagination.PageNo > 0 ? request.Pagination.PageNo : 1;
            int pageSize = request.Pagination.PageSize > 0 ? request.Pagination.PageSize : 10;


            var stocks = await _stockRepo.GetAllStocksAsync(pageNo, pageSize);
            var response = new StocksResponse();

            foreach (var stock in stocks)
            {
                response.Stocks.Add(new Stock
                {
                    Id = stock.Id,
                    RegistrationNo = stock.RegistrationNo,
                    Image = stock.Image,
                    MakeId = stock.MakeId,
                    FuelId = stock.FuelId,
                    ModelId = stock.ModelId,
                    Price = (double)stock.Price,
                    CityId = stock.CityId,
                    FuelName = stock.FuelType,
                    CityName = stock.CityName,
                    MakeName = stock.MakeName,
                    ModelName = stock.ModelName
                });
            }

            return response;
        }

        public override async Task<CitiesResponse> GetAllCities( Empty request  , ServerCallContext context)
        {
            var cities = await _stockRepo.GetAllCitiesAsync();
            var response = new CitiesResponse();

            foreach(var city in cities){
                response.Cities.Add(
                    new City {
                        Id = city.Id,
                        CityName = city.CityName
                    }
                );
            }

            return response;
        }

        public override async Task<MakeNamesResponse> GetAllMakeNames(Empty request, ServerCallContext context)
        {
            var makeNames = await _stockRepo.GetAllMakeNamesAsync();
            var response = new MakeNamesResponse();

            foreach (var make in makeNames)
            {
                response.Makes.Add(new GrpcServices.Make
                {
                    Id = make.Id,
                    MakeName = make.MakeName
                });
            }

            return response;
        }

        public override async Task<ModelsResponse> GetAllModelsByMakeId(MakeRequest request, ServerCallContext context)
        {
            var models = await _stockRepo.GetAllModelsByMakeIdAsync(request.MakeId);
            var response = new ModelsResponse();

            foreach (var model in models)
            {
                response.Models.Add(new GrpcServices.Model
                {
                    Id = model.Id,
                    MakeId = model.MakeId,
                    ModelName = model.ModelName,
                    MaxAllowedPrice = (double)model.MaxAllowedPrice,
                    MinAllowedPrice = (double)model.MinAllowedPrice,
                });
            }

            return response;
        }

       
        public override async Task<StocksResponse> GetStocksByFilter(StocksRequest request, ServerCallContext context)
        {   
            try
            {
                // Console.WriteLine($"Received request: {request?.Filter?.Budget}, {request?.Filter?.FuelTypes}");
                int pageNo = request.Pagination.PageNo > 0 ? request.Pagination.PageNo : 1;
                int pageSize = request.Pagination.PageSize > 0 ? request.Pagination.PageSize : 10;

                var filters = new Filters
                {
                    Budget = request?.Filter?.Budget ?? "",
                    FuelTypes = request?.Filter?.FuelTypes ?? "",
                    CityId = request?.Filter?.CityId ?? 0
                };

                // Console.WriteLine($"Created filters: Budget={filters.Budget}, FuelTypes={filters.FuelTypes}");

                
                var stocks = await _stockRepo.GetStocksByFilterAsync(filters, pageNo, pageSize);
                var response = new StocksResponse();

                foreach (var stock in stocks)
                {
                    response.Stocks.Add(new Stock
                    {
                        Id = stock.Id,
                        MakeId = stock.MakeId,
                        FuelId = stock.FuelId,
                        ModelId = stock.ModelId,
                        Price = (double)stock.Price,
                        CityId = stock.CityId,
                        RegistrationNo = stock.RegistrationNo, 
                        Image = stock.Image
                    });
                }

                return response;
            }
            catch (Exception ex)
            {
                // Console.WriteLine($"Error in GetStocksByFilter: {ex.Message}");
                // Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

       
        public override async Task<Stock> CreateStock(StockCreationRequest request, ServerCallContext context)
        {   

            if (string.IsNullOrWhiteSpace(request.Image))
            {
                request.Image = "https://images.pexels.com/photos/1670457/pexels-photo-1670457.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2";
            }

            // Map gRPC request to entity
            var stockEntity = _mapper.Map<StocksCars>(request);

            var createdStock = await _stockRepo.CreateStockAsync(stockEntity);

            return new Stock
            {
                Id = createdStock.Id,
                MakeId = createdStock.MakeId,
                FuelId = createdStock.FuelId,
                ModelId = createdStock.ModelId,
                Price = (double)createdStock.Price,
                CityId = createdStock.CityId,
                RegistrationNo = createdStock.RegistrationNo, 
                Image = createdStock?.Image ?? "https://images.pexels.com/photos/1670457/pexels-photo-1670457.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2"
            };
        }

       
        public override async Task<Empty> DeleteStock(StockDeleteRequest request, ServerCallContext context)
        {
            var success = await _stockRepo.DeleteStockAsync(request.Id);

            if (!success)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Stock with ID {request.Id} not found"));
            }

            return new Empty();
        }
    }
}
