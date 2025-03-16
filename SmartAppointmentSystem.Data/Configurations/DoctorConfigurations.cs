using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Data.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnType("uuid");

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(d => d.PasswordHash)
                .IsRequired();

            builder.Property(d => d.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(d => d.Image)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(d => d.CreatedAt)
                .IsRequired();


            builder.HasOne(d => d.Branch)
                .WithMany()
                .HasForeignKey(d => d.BranchId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
