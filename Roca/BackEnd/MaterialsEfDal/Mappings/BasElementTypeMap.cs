using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class BasElementTypeMap : EntityTypeConfiguration<BasElementType>
    {
        public BasElementTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            // Table & Column Mappings
            this.ToTable("BAS_ELEMENT_TYPES", "ROCA");
            this.Property(t => t.Id).HasColumnName("BAS_ELEMENT_TYPE_ID");
            this.Property(t => t.Code).HasColumnName("CODE");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.ClassId).HasColumnName("BAS_CLASS_ID");

            this.HasRequired(t => t.Class)
                .WithMany()
                .HasForeignKey(t => t.ClassId);

            this.HasMany(t => t.FieldDefinitions)
                .WithRequired()
                .HasForeignKey(d => d.ElementTypeId);

            this.HasMany(t => t.Specialties)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("BAS_ELEMENT_TYPE_SPECIALTY", "ROCA");
                    m.MapLeftKey("BAS_ELEMENT_TYPE_ID");
                    m.MapRightKey("SPECIALTY_ID");
                });
        }
    }
}
