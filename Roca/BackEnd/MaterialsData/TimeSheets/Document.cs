using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.TimeSheets
{
    public class Document : Entity<int>
    {
        public int ProjectId { get; set; }
        public int SpecialtyId { get; set; }
        public int TypeId { get; set; }
        public string DocNumber { get; set; }
        public string Title { get; set; }


        public Project Project { get; set; }
        public Specialty Specialty { get; set; }
        public LookUp Type { get; set; }
    }
}
