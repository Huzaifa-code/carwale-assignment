using AutoMapper;
using StocksApi.DAL.Entities;
using StocksApi.Dtos;

namespace StocksApi.Mappers
{
    public class StocksMappingProfile : Profile
    {
        public StocksMappingProfile()
        {
            // Mapping from Entity to DTO
            CreateMap<StocksCars, StocksDto>()
                .ForMember(dest => dest.FormattedName, 
                           opt => opt.MapFrom(src => $"{src.MakeYear} {src.MakeName} {src.ModelName}"))
                .ForMember(dest => dest.FormattedPrice, 
                           opt => opt.MapFrom(src => FormatPrice(src.Price)));

            // For Create - Adding data
            CreateMap<CreateStocksDto, StocksCars>();

            // For update
            CreateMap<UpdateStocksDto, StocksCars>();


        }

        private static string FormatPrice(decimal price)
        {
            if (price >= 10000000)                      // ## -> To show upto 2 decimal points
                return $"{price / 10000000.0m:0.##}Cr"; // m - for decimal
            else if (price >= 100000)
                return $"{price / 100000.0m:0.##}L";
            else
                return $"{price:#,##0}";
        }
    }
}