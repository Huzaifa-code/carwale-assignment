using AutoMapper;
using Carwale.Dtos;
using Carwale.GrpcServices;


namespace Carwale.Mappers
{
    public class MakeModelMappingProfile : Profile
    {
        public MakeModelMappingProfile(){
            CreateMap<MakeNamesResponse, IEnumerable<MakeNameDto>>();
            CreateMap<ModelsResponse, IEnumerable<ModelDto>>();


            // Not Needed right now :
            CreateMap<Make, MakeNameDto>();
            CreateMap<Model, ModelDto>();
            
        }
    }
}