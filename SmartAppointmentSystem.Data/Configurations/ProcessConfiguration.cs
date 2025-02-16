using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Data.Configurations
{
    public class ProcessConfiguration : IEntityTypeConfiguration<Process>
    {
        public void Configure(EntityTypeBuilder<Process> builder)
        {
            builder.ToTable("Processes");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid");

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.Duration)
                .IsRequired();

            builder.Property(s => s.DoctorId).HasColumnType("uuid");
            builder.HasOne(s => s.Doctor)
                .WithMany(d => d.Processes)
                .HasForeignKey(s => s.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
