using AcerPro.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AcerPro.Persistence
{
    public class QueryDatabaseContext : DbContext
    {
        public QueryDatabaseContext
            (DbContextOptions<QueryDatabaseContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
