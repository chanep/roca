using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public partial class MlItem : Entity<int>
    {
        public int MlId { get; set; }
        public int MaterialId { get; set; }
        public double Quantity { get; set; }
        public double PrevQuantity { get; set; }
        public virtual MaterialList MaterialList { get; set; }
        public virtual Material Material { get; set; }
    }
}
