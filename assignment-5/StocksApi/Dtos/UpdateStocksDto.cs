using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApi.Dtos
{
    public class UpdateStocksDto
    {   
       
        public required int Id { get; set; } 
        public required string MakeName { get; set; }
        public required string FuelType { get; set; }
        public required int Kms { get; set; }
        public required decimal Price { get; set; }
        public required string Location { get; set; }
        public required string City { get; set; }
        public required int MakeYear { get; set; }
        public required string ModelName { get; set; }
    }
}