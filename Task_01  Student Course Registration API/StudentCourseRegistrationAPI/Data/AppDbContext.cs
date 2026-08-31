using Microsoft.EntityFrameworkCore;
using StudentCourseRegistrationAPI.Models;

namespace StudentCourseRegistrationAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<StudentRegistration> StudentRegistrations { get; set; }
    }
}
