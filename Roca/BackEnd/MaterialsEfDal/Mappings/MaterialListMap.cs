using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class MaterialListMap : EntityTypeConfiguration<MaterialList>
    {
        public MaterialListMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnType("INT");

            this.Property(t => t.DocNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Revision)
                .IsRequired()
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("MATERIAL_LISTS", "ROCA");
            this.Property(t => t.Id).HasColumnName("ML_ID");
            this.Property(t => t.DocNumber).HasColumnName("DOC_NUMBER");
            this.Property(t => t.Title).HasColumnName("TITLE");
            this.Property(t => t.ProjectId).HasColumnName("PROJECT_ID");
            this.Property(t => t.SpecialtyId).HasColumnName("SPECIALTY_ID");
            this.Property(t => t.Revision).HasColumnName("REVISION");
            this.Property(t => t.PreviousRevisionMlId).HasColumnName("PREV_REV_ML_ID");
            this.Property(t => t.CreatorId).HasColumnName("CREATOR_ID");
            this.Property(t => t.CreatedOn).HasColumnName("CREATED_ON");
            this.Property(t => t.UpdaterId).HasColumnName("UPDATER_ID");
            this.Property(t => t.UpdatedOn).HasColumnName("UPDATED_ON");
            this.Property(t => t.RevisorId).HasColumnName("REVISOR_ID");
            this.Property(t => t.ApproverId).HasColumnName("APPROVER_ID");
            this.Property(t => t.DeletedInt).HasColumnName("DELETED").HasColumnType("INT");
            this.Property(t => t.StatusInt).HasColumnName("STATUS");
            this.Ignore(t => t.Deleted);
            this.Ignore(t => t.Status);
            this.Property(t => t.Purpose).HasColumnName("PURPOSE");

            // Relationships
            this.HasRequired(t => t.Project)
                .WithMany()
                .HasForeignKey(d => d.ProjectId);
            this.HasRequired(t => t.Specialty)
                .WithMany()
                .HasForeignKey(d => d.SpecialtyId);
            this.HasOptional(t => t.PreviousRevisionMl)
                .WithMany()
                .HasForeignKey(d => d.PreviousRevisionMlId);
            this.HasRequired(t => t.Creator)
                .WithMany()
                .HasForeignKey(d => d.CreatorId);
            this.HasOptional(t => t.Updater)
                .WithMany()
                .HasForeignKey(d => d.UpdaterId);
            this.HasOptional(t => t.Revisor)
                .WithMany()
                .HasForeignKey(d => d.RevisorId);
            this.HasOptional(t => t.Approver)
                .WithMany()
                .HasForeignKey(d => d.ApproverId);

        }
    }
}
