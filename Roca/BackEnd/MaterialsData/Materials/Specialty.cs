using System.Collections.Generic;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public partial class Specialty : Entity<int>
    {
        public string Abbreviation { get; set; }
        public string Name { get; set; }
    }
}
