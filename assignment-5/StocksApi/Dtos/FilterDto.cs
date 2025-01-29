
using System.ComponentModel.DataAnnotations;

namespace StocksApi.Dtos
{
    public class FilterDto
    {           
        // TODO : update this validation to allow decimals like 1.5, 7.5-11.5
        [RegularExpression(@"^\d+(-\d+)?$", ErrorMessage = "Budget must be in the format 'min-max' or a single number.")]
        public string? Budget { get; set; }

        // TODO : Add validation to check  FuelType int is between 1 and 6
        public string? FuelTypes { get; set; }
    }
}