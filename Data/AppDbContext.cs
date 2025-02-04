using Microsoft.EntityFrameworkCore;
using AIResumePicker.Models;

namespace AIResumePicker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Resume> Resumes { get; set; }
    }
}
