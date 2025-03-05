using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Data.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Ratings");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasColumnType("uuid");

            builder.Property(r => r.Score)
                .IsRequired();

            builder.Property(r => r.Comment)
                .HasMaxLength(500);

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.Property(r => r.DoctorId).HasColumnType("uuid");
            builder.HasOne(r => r.Doctor)
                .WithMany(d => d.Ratings)
                .HasForeignKey(r => r.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.PatientId).HasColumnType("uuid");
            builder.HasOne(r => r.Patient)
                .WithMany(p => p.Ratings)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
