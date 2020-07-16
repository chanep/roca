using System.Data.Entity.ModelConfiguration;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class PMaterialMap : EntityTypeConfiguration<PMaterial>
    {
        public PMaterialMap()
        {

            // Table & Column Mappings
            this.ToTable("PMATERIALS", "ROCA");
            this.Property(t => t.Lenght).HasColumnName("LENGHT");

        }
    }
}
