using SmartAppointmentSystem.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SmartAppointmentSystem.Data.Configurations;

public class AppointmentConfiguration : EntityTypeConfiguration<Appointment>
{
    public AppointmentConfiguration()
    {
        ToTable("Appointments");
        HasKey(a => a.Id);

        Property(a => a.DateTime)
            .IsRequired();

        Property(a => a.Status)
            .IsRequired()
            .HasMaxLength(50);

        Property(a => a.Notes)
            .HasMaxLength(500);

        HasRequired(a => a.Professional)
            .WithMany()
            .HasForeignKey(a => a.ProfessionalId);

        HasRequired(a => a.Customer)
            .WithMany(u => u.Appointments)
            .HasForeignKey(a => a.CustomerId);
    }
}
