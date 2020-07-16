using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class EiMaterialDetails : Entity<int>
    {
        public string LongDescription { get; set; }
    }
}
