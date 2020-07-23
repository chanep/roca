using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public class MatPiping : Entity<int>
    {
        public string ProjectId { get; set; }
        public string SpecType { get; set; }
        public string CommodityCode { get; set; }
        public string OepCode { get; set; }
        public string IdentLayout { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public double Quantity { get; set; }
        public double TotalQuantity { get; set; }
    }
}
