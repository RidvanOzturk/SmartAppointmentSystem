using SmartAppointmentSystem.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SmartAppointmentSystem.Data.Configurations;

public class ServiceConfiguration : EntityTypeConfiguration<Service>
{
    public ServiceConfiguration()
    {
        ToTable("Services");
        HasKey(s => s.Id);

        Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(150);

        Property(s => s.Duration)
            .IsRequired();

        HasRequired(s => s.Professional)
            .WithMany()
            .HasForeignKey(s => s.ProfessionalId);
    }
}
