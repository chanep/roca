using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    class BasCodeFieldMap : EntityTypeConfiguration<BasCodeField>
    {
        public BasCodeFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            // Table & Column Mappings
            this.ToTable("BAS_CODE_FIELDS", "ROCA");
            this.Property(t => t.Id).HasColumnName("BAS_CODE_FIELD_ID");
            this.Property(t => t.ElementId).HasColumnName("BAS_ELEMENT_ID");
            this.Property(t => t.BasCodeId).HasColumnName("BAS_CODE_ID");
            this.Property(t => t.FieldDefinitionId).HasColumnName("BAS_FIELD_DEFINITION_ID");

            this.HasRequired(t => t.BasCode)
                .WithMany()
                .HasForeignKey(d => d.BasCodeId);

            this.HasRequired(t => t.FieldDefinition)
                .WithMany()
                .HasForeignKey(d => d.FieldDefinitionId);
        }

    }
}
