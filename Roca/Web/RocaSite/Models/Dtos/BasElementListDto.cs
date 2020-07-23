using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.Web.RocaSite.Models.Dtos
{
    public class BasElementListDto
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string FullCode { get; set; }
        public string Description { get; set; }
        public string FamilyDescription { get; set; }
        public string DimensionalDescription { get; set; }
        public string Unit { get; set; }
        public string Observations { get; set; }
    }
}