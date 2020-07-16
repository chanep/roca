using System;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.Web.RocaSite.Models.Dtos
{
    public class MlItemDto : Dto<MlItem, MlItemDto>
    {
        public int Id { get; set; }
        public int MlId { get; set; }
        public int MaterialId { get; set; }
        public string MaterialIdentCode { get; set; }
        public string MaterialDescription { get; set; }
        public double Quantity { get; set; }
        public double PrevQuantity { get; set; }

        public double Difference
        {
            get { return Math.Round(Quantity - PrevQuantity, 8); }
        }

        public int MaterialUnitId { get; set; }
        public string MaterialUnitAbbreviation { get; set; }


        public MlItemDto(MlItem mlItem):base(mlItem)
        {
        }

        public MlItemDto()
        {
        }

    }
}