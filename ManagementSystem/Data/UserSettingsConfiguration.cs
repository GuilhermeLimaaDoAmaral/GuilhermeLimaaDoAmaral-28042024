using ManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementSystem.Data
{
    public class UserSettingsConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId); 
            builder.Property(u => u.Username).IsRequired(); 
            builder.Property(u => u.Password).IsRequired(); 
            builder.Property(u => u.IsActive).IsRequired();
        }
    }
}
