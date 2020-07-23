using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    class BasFieldDefinitionMap : EntityTypeConfiguration<BasFieldDefinition>
    {
        public BasFieldDefinitionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            // Table & Column Mappings
            this.ToTable("BAS_FIELD_DEFINITIONS", "ROCA");
            this.Property(t => t.Id).HasColumnName("BAS_FIELD_DEFINITION_ID");
            this.Property(t => t.ElementTypeId).HasColumnName("BAS_ELEMENT_TYPE_ID");
            this.Property(t => t.TypeInt).HasColumnName("TYPE").HasColumnType("INT");
            this.Property(t => t.Order).HasColumnName("FIELD_ORDER").HasColumnType("INT");
            this.Property(t => t.Code).HasColumnName("CODE");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.Length).HasColumnName("LENGTH").HasColumnType("INT");
        }

    }
}
