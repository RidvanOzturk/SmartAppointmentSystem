using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Data.Configurations;

public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
{
    public void Configure(EntityTypeBuilder<TimeSlot> builder)
    {
        builder.ToTable("TimeSlots");

        builder.HasKey(ts => ts.Id);

        builder.Property(ts => ts.AvailableFrom)
            .IsRequired();

        builder.Property(ts => ts.AvailableTo)
            .IsRequired();

        builder.HasOne(ts => ts.Process)
            .WithMany(p => p.TimeSlots)
            .HasForeignKey(ts => ts.ProcessId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ts => ts.Doctor)
            .WithMany()
            .HasForeignKey(ts => ts.DoctorId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}

