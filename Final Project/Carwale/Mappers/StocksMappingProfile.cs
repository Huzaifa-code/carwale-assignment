using AutoMapper;
using Carwale.Dtos;
using Carwale.GrpcServices;

namespace Carwale.Mappers
{
    public class StocksMappingProfile : Profile
    {
        public StocksMappingProfile()
        {   

            // Map FilterDto to StocksRequest (gRPC request type)
            CreateMap<FilterDto, StocksRequest>();
            
            // grpc server Stock to StocksDto
            CreateMap<Stock, StocksDto>();
            CreateMap<CreateStocksDto,StockCreationRequest>();



            // Add a mapping for StocksCars to StockCreationRequest
            // CreateMap<StocksCars, StockCreationRequest>();

            // CreateMap<CreateStocksDto, StocksCars>();
            

        }
    }
}