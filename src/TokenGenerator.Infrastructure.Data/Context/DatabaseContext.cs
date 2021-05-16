using Microsoft.EntityFrameworkCore;
using TokenGenerator.Domain.Models;
using TokenGenerator.Infrastructure.Data.Configuration;

namespace TokenGenerator.Infrastructure.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {   }

        public DbSet<Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TokenGenerator");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CardConfiguration());
        }
    }
}
