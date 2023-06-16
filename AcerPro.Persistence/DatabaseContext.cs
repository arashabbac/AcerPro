using AcerPro.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Framework.Domain;

namespace AcerPro.Persistence;

public class DatabaseContext : DbContext
{
    public DatabaseContext
        (DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is IEntity &&
                        (e.State == EntityState.Added ||
                        e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            entityEntry.Property("ModifiedDateTime").CurrentValue = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property("InsertDateTime").CurrentValue = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
