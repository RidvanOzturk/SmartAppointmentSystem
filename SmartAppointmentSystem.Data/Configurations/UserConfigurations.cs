using SmartAppointmentSystem.Data.Entities;
using System.Data.Entity.ModelConfiguration;
namespace SmartAppointmentSystem.Data.Configurations;
public class UserConfiguration : EntityTypeConfiguration<User>
{
    public UserConfiguration()
    {
        ToTable("Users");
        HasKey(u => u.Id);

        Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        Property(u => u.Mail)
            .IsRequired()
            .HasMaxLength(200);

        Property(u => u.PasswordHash)
            .IsRequired();

        Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(50);

        HasMany(u => u.Appointments)
            .WithRequired(a => a.Customer)
            .HasForeignKey(a => a.CustomerId);

        HasMany(u => u.Ratings)
            .WithRequired(r => r.Customer)
            .HasForeignKey(r => r.CustomerId);
    }
}
