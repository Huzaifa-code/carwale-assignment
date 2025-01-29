using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StocksApi.DAL.Entities;
using StocksApi.Dtos;

namespace StocksApi.Mappers
{
    public class FiltersMappingProfile : Profile
    {   
        public FiltersMappingProfile(){
            CreateMap<FilterDto, Filters>(); 
        }
    }
}