using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class BasClassAttribute : Entity<int>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Property { get; set; }
        public int ClassId { get; set; }

    }
}
