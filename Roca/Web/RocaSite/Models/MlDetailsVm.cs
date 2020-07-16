using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Cno.Roca.Web.RocaSite.Models.Dtos;

namespace Cno.Roca.Web.RocaSite.Models
{
    public class MlDetailsVm
    {
        public ViewMode ViewMode { get; set; }
        public bool SaveSucceed { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Specialty> Specialties { get; set; }
        public IEnumerable<string> Purposes { get; set; } 

        public MaterialListDto MaterialList { get; set; }
    }
}