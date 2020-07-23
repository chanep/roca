using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.Web.RocaSite.Models.Dtos
{
    public class BasElementDetailsDto
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string  FullCode { get; set; }
        public string Unit { get; set; }
        public string Observations { get; set; }
        public double Weight { get; set; }
        public IList<BasCodeFieldDto> Fields { get; set; }
        public IList<BasClassAttributeWithValue> ExtraAttributes { get; set; }

        public BasElementDetailsDto()
        {
            Fields = new List<BasCodeFieldDto>();
            ExtraAttributes = new List<BasClassAttributeWithValue>();
        }
    }
}