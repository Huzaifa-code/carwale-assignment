using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwale.Dtos
{
    public class MakeNameDto
    {
        public int Id { get; set; }
        public required string MakeName { get; set; }
    }
}