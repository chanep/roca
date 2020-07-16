using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).HasColumnType("INT");


            // Table & Column Mappings
            this.ToTable("PROJECTS", "ROCA");
            this.Property(t => t.Id).HasColumnName("PROJECT_ID");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.Code).HasColumnName("CODE");
            this.Property(t => t.ShortName).HasColumnName("SHORT_NAME");
            this.Property(t => t.SubprojectType).HasColumnName("SUBPROJECT_TYPE");
            this.Property(t => t.ParentId).HasColumnName("PARENT_ID");

            // Relationships
            this.HasMany(t => t.Subprojects)
                .WithOptional()
                .HasForeignKey(d => d.ParentId);

            this.HasOptional(t => t.Parent)
                .WithMany()
                .HasForeignKey(d => d.ParentId);
        }
    }
}
