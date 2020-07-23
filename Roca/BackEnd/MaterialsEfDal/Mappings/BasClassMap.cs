using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class BasClassMap : EntityTypeConfiguration<BasClass>
    {
        public BasClassMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id).HasColumnName("BAS_CLASS_ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            // Table & Column Mappings
            this.ToTable("BAS_CLASSES", "ROCA");
            this.Property(t => t.Name).HasColumnName("NAME");

            this.HasMany(t => t.ExtraAttributes)
                .WithRequired()
                .HasForeignKey(d => d.ClassId);
        }
    }
}
