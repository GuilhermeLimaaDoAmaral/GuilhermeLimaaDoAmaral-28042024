using ManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementSystem.Data
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.HasKey(u => u.UnitId);
            builder.Property(u => u.Code).IsRequired();
            builder.Property(u => u.Code).HasMaxLength(50);
            builder.HasIndex(u => u.Code).IsUnique();
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.IsActive).IsRequired();
        }
    }
}

