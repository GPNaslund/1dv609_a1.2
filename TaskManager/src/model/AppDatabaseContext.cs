using Microsoft.EntityFrameworkCore;

namespace TaskManager.src.model
{
    public class AppDatabaseContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}