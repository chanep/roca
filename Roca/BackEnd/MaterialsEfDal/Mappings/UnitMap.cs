using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class UnitMap : EntityTypeConfiguration<Unit>
    {
        public UnitMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).HasColumnType("INT");

            this.Property(t => t.Abbreviation)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UNITS", "ROCA");
            this.Property(t => t.Id).HasColumnName("UNIT_ID");
            this.Property(t => t.Abbreviation).HasColumnName("ABBREVIATION");
            this.Property(t => t.Name).HasColumnName("NAME");
        }
    }
}
