using Carwale.DAL.Enums;

namespace Carwale.DAL.Entities
{
    public class Filters
    {
        public string? Budget { get; set; }
        public string? FuelTypes { get; set; }
        public int? CityId { get; set; }
    }
}