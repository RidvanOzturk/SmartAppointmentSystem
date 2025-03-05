using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Data.Configurations;

public class LogConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.ToTable("Logs");

        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(l => l.Request)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(l => l.Headers)
            .HasColumnType("text");

        builder.Property(l => l.Endpoint)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(l => l.HttpMethod)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(l => l.Response)
            .HasColumnType("text");

        builder.Property(l => l.Ip)
            .IsRequired()
            .HasMaxLength(45);

        builder.Property(l => l.CreatedAt)
            .IsRequired();
    }
}
