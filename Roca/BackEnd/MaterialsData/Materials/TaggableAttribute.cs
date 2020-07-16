using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    /// <summary>
    /// Atributo de un material tagueable
    /// </summary>
    public class TaggableAttribute : Entity<int>
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
    }
}
