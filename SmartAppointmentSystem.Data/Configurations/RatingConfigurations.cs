using SmartAppointmentSystem.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SmartAppointmentSystem.Data.Configurations;

public class RatingConfiguration : EntityTypeConfiguration<Rating>
{
    public RatingConfiguration()
    {
        ToTable("Ratings");
        HasKey(r => r.Id);

        Property(r => r.Score)
            .IsRequired();

        Property(r => r.Comment)
            .HasMaxLength(500);

        Property(r => r.CreatedAt)
            .IsRequired();

        HasRequired(r => r.Professional)
            .WithMany()
            .HasForeignKey(r => r.ProfessionalId);

        HasRequired(r => r.Customer)
            .WithMany()
            .HasForeignKey(r => r.CustomerId);
    }
}
