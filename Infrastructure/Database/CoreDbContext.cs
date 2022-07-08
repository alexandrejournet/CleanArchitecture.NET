using CleanArchitecture.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Database
{
    public class CoreDbContext : DbContext
    {

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        { }

        public DbSet<Mot> Mot { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
