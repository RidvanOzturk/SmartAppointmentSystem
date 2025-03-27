using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Data.Configurations;

public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.Id).UseIdentityColumn();
        builder.Property(rt => rt.Token)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(rt => rt.Expiration)
            .IsRequired();
        builder.Property(rt => rt.PatientId)
            .HasColumnType("uuid")
            .IsRequired(false);
        builder.Property(rt => rt.DoctorId)
            .HasColumnType("uuid")
            .IsRequired(false);
        builder.Property(rt => rt.IsRevoked)
            .IsRequired();
        builder.Property(rt => rt.CreatedAt)
            .IsRequired();
        builder.Property(rt => rt.RevokedAt)
            .IsRequired(false);

        builder.HasOne(rt => rt.Patient)
               .WithMany(p => p.RefreshTokens)
               .HasForeignKey(rt => rt.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(rt => rt.Doctor)
               .WithMany(d => d.RefreshTokens)
               .HasForeignKey(rt => rt.DoctorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
