using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwale.DAL.Entities
{
    public class Make
    {
        public int Id { get; set; }
        public required string MakeName { get; set; }
    }
}