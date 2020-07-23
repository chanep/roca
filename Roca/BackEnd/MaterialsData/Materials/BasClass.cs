using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class BasClass : Entity<int>
    {
        public string Name { get; set; }
        public ICollection<BasClassAttribute> ExtraAttributes { get; set; }

        public BasClass()
        {
            ExtraAttributes = new List<BasClassAttribute>();
        }
    }


}
