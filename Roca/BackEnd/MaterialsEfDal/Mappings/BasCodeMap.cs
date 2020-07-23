using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    class BasCodeMap : EntityTypeConfiguration<BasCode>
    {
        public BasCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            // Table & Column Mappings
            this.ToTable("BAS_CODES", "ROCA");
            this.Property(t => t.Id).HasColumnName("BAS_CODE_ID");
            this.Property(t => t.Field).HasColumnName("FIELD");
            this.Property(t => t.Code).HasColumnName("CODE");
            this.Property(t => t.Description).HasColumnName("DESCRIPTION");
            this.Property(t => t.ShortDescription).HasColumnName("SHORT_DESCRIPTION");
        }

    }
}
