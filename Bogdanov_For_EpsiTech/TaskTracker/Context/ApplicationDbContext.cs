using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;

namespace TaskTracker.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<MyTask> Tasks { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}
