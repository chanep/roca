using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class BasClassAttributeMap : EntityTypeConfiguration<BasClassAttribute>
    {
        public BasClassAttributeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id).HasColumnName("BAS_CLASS_ATTRIBUTE_ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            // Table & Column Mappings
            this.ToTable("BAS_CLASS_ATTRIBUTES", "ROCA");
            this.Property(t => t.ClassId).HasColumnName("BAS_CLASS_ID");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.Type).HasColumnName("TYPE");
            this.Property(t => t.Property).HasColumnName("PROPERTY");

        }
    }
}
