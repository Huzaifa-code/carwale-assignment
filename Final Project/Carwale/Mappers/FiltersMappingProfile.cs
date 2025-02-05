using AutoMapper;
using Carwale.DAL.Entities;
using Carwale.Dtos;
using Carwale.GrpcServices;

namespace Carwale.Mappers
{
    public class FiltersMappingProfile : Profile
    {   
        public FiltersMappingProfile(){
            
            CreateMap<FilterDto,StocksRequest>(); 

            CreateMap<FilterDto, Filters>(); 
        }
    }
}