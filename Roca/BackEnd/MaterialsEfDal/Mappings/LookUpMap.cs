using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    class LookUpMap : EntityTypeConfiguration<LookUp>
    {
        public LookUpMap()
        {       
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("LOOKUPS", "ROCA");
            this.Property(t => t.Id).HasColumnName("LOOKUP_ID");
            this.Property(t => t.Type).HasColumnName("TYPE");
            this.Property(t => t.Code).HasColumnName("CODE");
            this.Property(t => t.Value).HasColumnName("VALUE");
        }
    }
}
