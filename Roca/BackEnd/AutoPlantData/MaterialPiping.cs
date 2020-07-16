using System;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.AutoPlant.Data
{
    public class MaterialPiping : Entity<int>
    {        
        public string Service { get; set; }
        public string Line { get; set; }
        public string Tag { get; set; }
        public string NominalDiam { get; set; }
        public string Class { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Material { get; set; }
        public string Rating { get; set; }
        public string Schedule { get; set; }
        public string PaintCode { get; set; }
        public string Insulation { get; set; }
        public string PieceMark { get; set; }
        public double Quantity { get; set; }
        public double TotalQuantity { get; set; }

        public double QuantityByOthers
        {
            get { return Math.Round(TotalQuantity - Quantity, 8); }
        }
        
    }
}
