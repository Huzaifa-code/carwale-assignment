using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwale.Dtos
{
    public class CreateStocksDto
    {   
        public required string RegistrationNo { get; set; }
        public string? Image { get; set; }
        public required int MakeId { get; set; }  
        public required int FuelId { get; set; }  
        public required decimal Price { get; set; }
        public required int CityId { get; set; }  
        public required int ModelId { get; set; }  
    }
}