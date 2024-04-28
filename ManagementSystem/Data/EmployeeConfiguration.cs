using ManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementSystem.Data
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeId); 
            builder.Property(e => e.Name).IsRequired();
            builder.HasOne(e => e.Unit) 
                .WithMany() 
                .HasForeignKey(e => e.UnitId) 
                .IsRequired(); 
            builder.HasOne(e => e.User) 
                .WithMany() 
                .HasForeignKey(e => e.UserId) 
                .IsRequired(); 
        }
    }
    

}
