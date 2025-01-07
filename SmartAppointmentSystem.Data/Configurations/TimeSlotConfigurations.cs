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
            .WithMany(s => s.TimeSlots)
            .HasForeignKey(ts => ts.ProfessionalId);
    }
}
