using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.Web.RocaSite.Models.Dtos
{
    public class MatPipingFamilyDto
    {
        public string CommodityCode { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        public ICollection<MatPipingDto> Materials { get; set; }

        public MatPipingFamilyDto(IEnumerable<MatPiping> entities)
        {
            Materials = new List<MatPipingDto>();

            foreach (var entity in entities)
            {
                if (CommodityCode == null && ShortDescription == null)
                {
                    CommodityCode = entity.CommodityCode;
                    ShortDescription = entity.ShortDescription;
                    LongDescription = entity.LongDescription;
                }

                var mat = new MatPipingDto(entity);
                Materials.Add(mat);
            }
        }
    }
}