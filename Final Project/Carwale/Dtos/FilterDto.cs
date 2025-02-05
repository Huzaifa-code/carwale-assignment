
using System.ComponentModel.DataAnnotations;

namespace Carwale.Dtos
{
    public class FilterDto
    {           
        [RegularExpression(@"^\d+(\.\d+)?(-\d+(\.\d+)?)?$", ErrorMessage = "Budget must be in the format 'min-max' or a single number.")]
        public string? Budget { get; set; }

        [FuelTypesValidation(ErrorMessage = "FuelTypes must be in the format '1', '1+2', '2+3+4', etc., and each value must be between 1 and 6.")]
        public string? FuelTypes { get; set; }

        public int? CityId { get; set; }
    }


    public class FuelTypesValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            string fuelTypes = value.ToString()!;

            var fuelTypeValues = fuelTypes.Split('+');

            foreach (var fuelTypeValue in fuelTypeValues)
            {
                
                if (!int.TryParse(fuelTypeValue, out int fuelType))
                {
                    return new ValidationResult($"Invalid fuel type value: '{fuelTypeValue}'. Fuel types must be integers.");
                }

                
                if (fuelType < 1 || fuelType > 6)
                {
                    return new ValidationResult($"Fuel type '{fuelType}' must be between 1 and 6.");
                }
            }

           
            return ValidationResult.Success;
        }
    }
}