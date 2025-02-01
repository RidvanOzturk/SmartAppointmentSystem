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

        builder.HasData(
            new Branch { Id = 1, Title = "General Practitioner", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 2, Title = "Internal Medicine", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 3, Title = "Cardiology", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 4, Title = "Neurology", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 5, Title = "Orthopedics", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 6, Title = "Pediatrics", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 7, Title = "Dermatology", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 8, Title = "Ophthalmology", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 9, Title = "Otolaryngology", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 10, Title = "Urology", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 11, Title = "Gastroenterology", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 12, Title = "Pulmonology", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 13, Title = "Endocrinology", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 14, Title = "Psychiatry", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 15, Title = "General Surgery", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 16, Title = "Oncology", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 17, Title = "Nephrology", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) },
            new Branch { Id = 18, Title = "Rheumatology", Description = "", CreatedAt = new DateTime(2025, 2, 1), UpdatedAt = new DateTime(2025, 2, 1) }
        );
    }
}
