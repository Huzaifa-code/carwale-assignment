using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwale.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        public required string CityName { get; set; }
    }
}