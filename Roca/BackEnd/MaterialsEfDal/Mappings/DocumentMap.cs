using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class DocumentMap : EntityTypeConfiguration<Document>
    {
        public DocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            // Table & Column Mappings
            this.ToTable("DOCUMENTS", "ROCA");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.ProjectId).HasColumnName("PROJECT_ID");
            this.Property(t => t.SpecialtyId).HasColumnName("SPECIALTY_ID");
            this.Property(t => t.TypeId).HasColumnName("TYPE_ID");
            this.Property(t => t.DocNumber).HasColumnName("DOCNUMBER");
            this.Property(t => t.Title).HasColumnName("TITLE");

            // Relationships
            this.HasRequired(t => t.Project)
                .WithMany()
                .HasForeignKey(d => d.ProjectId);

            this.HasRequired(t => t.Specialty)
                .WithMany()
                .HasForeignKey(d => d.SpecialtyId);

            this.HasRequired(t => t.Type)
                .WithMany()
                .HasForeignKey(d => d.TypeId);

        }
    }
}
