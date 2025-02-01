using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Data.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.PasswordHash)
            .IsRequired();

        

        builder.Property(d => d.Description)
            .HasMaxLength(500);

        builder.Property(d => d.Image)
            .HasMaxLength(200);

        builder.Property(d => d.Polyclinic)
            .HasMaxLength(100);

        // İlişkiler
        builder.HasMany(d => d.Appointments)
            .WithOne(a => a.Doctor)
            .HasForeignKey(a => a.DoctorId);

        builder.HasMany(d => d.Ratings)
            .WithOne(r => r.Doctor)
            .HasForeignKey(r => r.DoctorId);

        builder.HasMany(d => d.Processes)
            .WithOne(p => p.Doctor)
            .HasForeignKey(p => p.DoctorId);

        builder.HasMany(d => d.TimeSlots)
            .WithOne(ts => ts.Doctor)
            .HasForeignKey(ts => ts.DoctorId);
    }
}
