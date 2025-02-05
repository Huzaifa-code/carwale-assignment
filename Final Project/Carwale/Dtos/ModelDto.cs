using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwale.Dtos
{
    public class ModelDto
    {
        public int Id { get; set; }
        public required string ModelName { get; set; }
        public int MakeId { get; set; }
        public double MaxAllowedPrice { get; set; }
        public double MinAllowedPrice { get; set; }
    }
}