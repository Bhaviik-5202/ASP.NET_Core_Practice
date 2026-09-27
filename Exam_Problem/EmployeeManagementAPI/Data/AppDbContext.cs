using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) 
        { 
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.EmployeeCode)
                    .IsRequired(false)
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Salary)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.JoiningDate)
                    .IsRequired()
                    .HasColumnType("datetime");

                entity.Property(e => e.BirthDate)
                    .IsRequired()
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnType("bit");
            });
        }
    }
}
