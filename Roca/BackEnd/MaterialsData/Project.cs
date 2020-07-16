using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data
{

    public class Project : Entity<int>
    {      
        public string Name { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string SubprojectType { get; set; }
        public int? ParentId { get; set; }

        public Project()
        {
            Subprojects = new List<Project>();
        }

        public ICollection<Project> Subprojects { get; set; }
        public Project Parent { get; set; }
    }
}
