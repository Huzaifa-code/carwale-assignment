using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwale.DAL.Entities
{
    public class Cities
    {
        public int Id { get; set; }
        public required string CityName { get; set; }
    }
}