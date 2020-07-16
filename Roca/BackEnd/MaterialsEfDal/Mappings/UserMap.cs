using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Cno.Roca.BackEnd.Materials.Data.Users;

namespace Cno.Roca.BackEnd.Materials.EfDal.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {

            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .HasColumnName("ID").HasColumnType("INT");

            Property(t => t.LongUserName)
                .HasColumnName("USER_NAME")
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName("NAME")
                .IsRequired();

            Property(t => t.LastName)
                .HasColumnName("LAST_NAME")
                .IsRequired();

            Property(t => t.Initials)
                .HasColumnName("INITIALS")
                .IsRequired();

            Property(t => t.Mail)
                .HasColumnName("MAIL")
                .IsRequired();

            Property(t => t.Roles)
                .HasColumnName("ROLES")
                .IsRequired();

            // Table & Column Mappings
            ToTable("USERS", "ROCA");


            // Relationships
            this.HasMany(t => t.Specialties)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("USER_SPECIALTY", "ROCA");
                    m.MapLeftKey("USER_ID");
                    m.MapRightKey("SPECIALTY_ID");
                });
        }
    }
}
