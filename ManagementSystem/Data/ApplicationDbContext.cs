using ManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<Employee> Employee { get; set; }

        public virtual DbSet<Unit> Unit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserSettingsConfiguration());

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

            modelBuilder.ApplyConfiguration(new UnitConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
