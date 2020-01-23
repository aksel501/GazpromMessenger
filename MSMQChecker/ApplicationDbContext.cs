using Microsoft.EntityFrameworkCore;

namespace GazpromMessenger.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .; Database = GazpromMessenger; Trusted_Connection = true; MultipleActiveResultSets = true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
