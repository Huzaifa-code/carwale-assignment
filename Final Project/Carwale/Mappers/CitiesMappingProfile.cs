using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Carwale.Dtos;
using Carwale.GrpcServices;

namespace Carwale.Mappers
{
    public class CitiesMappingProfile : Profile
    {   
        public CitiesMappingProfile(){

            // Map individual City objects to CitiesDto
            CreateMap<City, CityDto>(); 

            // Map CitiesResponse (which contains a repeated field of City) to a list of CitiesDto
            CreateMap<CitiesResponse, CitiesDto>()
                .ForMember(dest => dest.Cities, opt => opt.MapFrom(src => src.Cities.Select(city => new CityDto
                {
                    Id = city.Id,
                    CityName = city.CityName
                }).ToList()));
            
        }
       
    }
}