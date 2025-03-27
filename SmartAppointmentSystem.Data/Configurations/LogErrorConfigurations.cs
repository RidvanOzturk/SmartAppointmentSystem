using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Data.Configurations;

public class LogErrorConfigurations : IEntityTypeConfiguration<LogError>
{
    public void Configure(EntityTypeBuilder<LogError> builder)
    {
        builder.ToTable("LogErrors");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id)
            .HasColumnType("uuid")
            .IsRequired();
        builder.Property(l => l.ExceptionMessage)
            .IsRequired()
            .HasColumnType("text")
            .HasMaxLength(250);
        builder.Property(l => l.CreatedAt)
            .IsRequired();
    }
}
