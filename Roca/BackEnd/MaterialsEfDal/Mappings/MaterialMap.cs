using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class MaterialMap : EntityTypeConfiguration<Material>
    {
        public MaterialMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(100);


            // Table & Column Mappings
            this.ToTable("MATERIALS", "ROCA");
            this.Property(t => t.Id).HasColumnName("MATERIAL_ID");
            this.Property(t => t.IdentCode).HasColumnName("IDENT_CODE");
            this.Property(t => t.Description).HasColumnName("DESCRIPTION");
        }
    }
}
