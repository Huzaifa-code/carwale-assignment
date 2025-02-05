

namespace Carwale.Dtos
{
    public class CitiesDto
    {
        public CitiesDto(){
            Cities = new List<CityDto>();
        }

        public List<CityDto> Cities { get; set; }
    }
}