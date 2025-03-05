using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Data.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(b => b.Description)
               .HasMaxLength(500);

        builder.Property(b => b.CreatedAt)
               .IsRequired();

        builder.Property(b => b.UpdatedAt)
               .IsRequired();
    }
}
