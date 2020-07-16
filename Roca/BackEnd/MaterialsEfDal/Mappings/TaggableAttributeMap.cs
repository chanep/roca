using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class TaggableAttributeMap : EntityTypeConfiguration<TaggableAttribute>
    {
        public TaggableAttributeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            this.ToTable("TAGGABLE_ATTRIBUTES", "ROCA");
            this.Property(t => t.Id).HasColumnName("TAGGABLE_ATTRIBUTE_ID");
            this.Property(t => t.TypeId).HasColumnName("TAGGABLE_TYPE_ID");
            this.Property(t => t.Name).HasColumnName("NAME");

            // Relationships

        }
    }
}
