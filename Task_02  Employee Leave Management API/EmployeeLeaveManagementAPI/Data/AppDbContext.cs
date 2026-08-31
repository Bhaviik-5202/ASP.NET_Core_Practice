using EmployeeLeaveManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<LeaveApplication> LeaveApplications => Set<LeaveApplication>();
    }
}
