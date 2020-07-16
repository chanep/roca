using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class MlItemMap : EntityTypeConfiguration<MlItem>
    {
        public MlItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            // Table & Column Mappings
            this.ToTable("ML_ITEMS", "ROCA");
            this.Property(t => t.Id).HasColumnName("ML_ITEM_ID");
            this.Property(t => t.MlId).HasColumnName("ML_ID");
            this.Property(t => t.MaterialId).HasColumnName("MATERIAL_ID");
            this.Property(t => t.Quantity).HasColumnName("QUANTITY");
            this.Property(t => t.PrevQuantity).HasColumnName("QUANTITY_PREV");

            // Relationships
            this.HasRequired(t => t.MaterialList)
                .WithMany(t => t.Items)
                .HasForeignKey(d => d.MlId);
            this.HasRequired(t => t.Material)
                .WithMany()
                .HasForeignKey(d => d.MaterialId);

        }
    }
}
