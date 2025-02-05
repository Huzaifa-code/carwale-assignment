using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwale.DAL.Entities
{
    public class Model
    {
        public int Id { get; set; }
        public required string ModelName { get; set; }
        public int MakeId { get; set; }
        public decimal MaxAllowedPrice { get; set; }
        public decimal MinAllowedPrice { get; set; }
    }
}