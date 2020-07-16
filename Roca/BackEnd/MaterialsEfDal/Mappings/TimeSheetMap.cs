using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class TimeSheetMap : EntityTypeConfiguration<TimeSheet>
    {
        public TimeSheetMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            this.ToTable("TIMESHEETS", "ROCA");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.UserId).HasColumnName("USER_ID");
            this.Property(t => t.LeaderId).HasColumnName("LEADER_ID");
            this.Property(t => t.SpecialtyId).HasColumnName("SPECIALTY_ID");
            this.Property(t => t.ControlDate).HasColumnName("CONTROL_DATE");
            this.Property(t => t.Status).HasColumnName("STATUS");


            this.HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(d => d.UserId);

            this.HasOptional(t => t.Leader)
                .WithMany()
                .HasForeignKey(d => d.LeaderId);

            this.HasRequired(t => t.Specialty)
                .WithMany()
                .HasForeignKey(d => d.SpecialtyId);

        }
    }
}
