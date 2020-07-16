using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data
{
    public class LookUp : Entity<int>
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
    }
}
