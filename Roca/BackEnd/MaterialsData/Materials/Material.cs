using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Materials
{
    public partial class Material : Entity<int>
    {
        public string Description { get; set; }
        public string IdentCode { get; set; }
    }
}
