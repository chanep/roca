using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class BasCode : Entity<int>
    {
        public string Field { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
    }
}
