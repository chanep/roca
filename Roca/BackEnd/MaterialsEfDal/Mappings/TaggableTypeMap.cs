using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class TaggableTypeMap : EntityTypeConfiguration<TaggableType>
    {
        public TaggableTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            this.ToTable("TAGGABLE_TYPES", "ROCA");
            this.Property(t => t.Id).HasColumnName("TAGGABLE_TYPE_ID");
            this.Property(t => t.ParentId).HasColumnName("PARENT_ID");
            this.Property(t => t.SpecialtyId).HasColumnName("SPECIALTY_ID");
            this.Property(t => t.Name).HasColumnName("NAME");

            // Relationships
            this.HasRequired(t => t.Specialty)
                .WithMany()
                .HasForeignKey(d => d.SpecialtyId);

            this.HasMany(t => t.Subtypes)
                .WithOptional()
                .HasForeignKey(d => d.ParentId);

            this.HasMany(t => t.Attributes)
                .WithRequired()
                .HasForeignKey(d => d.TypeId);
        }
        
    }
}
