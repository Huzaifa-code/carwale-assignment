using AutoMapper;
using Carwale.DAL.Entities;
using Carwale.GrpcServices;


namespace Carwale.Services.Mappers
{
    public class StocksMappingProfile : Profile
    {
        public StocksMappingProfile(){
            // Map StockCreationRequest from client to server-side entity
            CreateMap<StockCreationRequest, StocksCars>();

            // Map StockDeleteRequest from client to server-side entity
            CreateMap<StockDeleteRequest, StocksCars>();

            // Map Stock response from entity to gRPC Stock response
            CreateMap<StocksCars, StocksResponse >();
        }
    }
}