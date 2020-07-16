using System.Data.Entity.ModelConfiguration;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class EiMaterialMap : EntityTypeConfiguration<EiMaterial>
    {
        public EiMaterialMap()
        {
            this.Property(t => t.Power)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("EIMATERIALS", "ROCA");
            this.Property(t => t.Power).HasColumnName("POWER");

            this.HasOptional(t => t.Details)
                .WithRequired();

        }
    }
}
