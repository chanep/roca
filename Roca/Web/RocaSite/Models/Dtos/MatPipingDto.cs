using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.Web.RocaSite.Models.Dtos
{
    public class MatPipingDto : Dto<MatPiping, MatPipingDto>
    {
        public string OepCode { get; set; }
        public string IdentLayout { get; set; }
        public double Quantity { get; set; }
        public double TotalQuantity { get; set; }

        public MatPipingDto(MatPiping entity)
            : base(entity)
        {
            Quantity = Math.Round(entity.Quantity, 8);
            TotalQuantity = Math.Round(entity.TotalQuantity, 8);
        }
        public MatPipingDto()
        {
            
        }
    }
}