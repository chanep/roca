using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.AutoPlant.Data
{
    public class MatPipingMigration : Entity<int>
    {
        public string ProjectId { get; set; }
        public string AreaId { get; set; }
        public string Area { get; set; }
        public string Service { get; set; }
        public string Line { get; set; }
        public string Tag { get; set; }
        public string PieceMark { get; set; }
        public double Quantity { get; set; }
        public double TotalQuantity { get; set; }

        public double QuantityByOthers
        {
            get { return Math.Round(TotalQuantity - Quantity, 8); }
        }

    }
}
