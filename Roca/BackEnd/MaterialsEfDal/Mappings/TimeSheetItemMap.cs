using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class TimeSheetItemMap : EntityTypeConfiguration<TimeSheetItem>
    {
        public TimeSheetItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            // Table & Column Mappings
            this.ToTable("TIMESHEET_ITEMS", "ROCA");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.TimeSheetId).HasColumnName("TIMESHEET_ID");
            this.Property(t => t.SubprojectId).HasColumnName("SUBPROJECT_ID");
            this.Property(t => t.DocumentId).HasColumnName("DOCUMENT_ID");
            this.Property(t => t.TaskId).HasColumnName("TASK_ID");
            this.Property(t => t.Hours).HasColumnName("HOURS");

            // Relationships
            this.HasRequired(t => t.TimeSheet)
                .WithMany(t => t.Items)
                .HasForeignKey(d => d.TimeSheetId);
            this.HasRequired(t => t.Subproject)
                .WithMany()
                .HasForeignKey(d => d.SubprojectId);
            this.HasOptional(t => t.Document)
                .WithMany()
                .HasForeignKey(d => d.DocumentId);
            this.HasOptional(t => t.Task)
                .WithMany()
                .HasForeignKey(d => d.TaskId);

        }
    }
}
