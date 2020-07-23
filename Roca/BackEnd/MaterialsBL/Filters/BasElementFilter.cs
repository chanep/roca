using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.Materials.BL.Filters
{
    public class BasElementFilter
    {
        public string FullCode { get; set; }
        public string Description { get; set; }
        public int? TypeId { get; set; }
        public int? SpecialtyId { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }
    }
}
