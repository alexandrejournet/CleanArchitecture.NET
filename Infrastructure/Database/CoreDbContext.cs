using CleanArchitecture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
