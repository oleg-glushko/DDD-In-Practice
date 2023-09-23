using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DddInPractice.Logic.SnackMachines;
using DddInPractice.Logic.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using DddInPractice.Logic.Atms;

namespace DddInPractice.Logic.Utils;

public class DddInPracticeDbContext : DbContext
{
    public DbSet<SnackMachine> SnackMachines { get; set; } = null!;
    public DbSet<Atm> Atms { get; set; } = null!;

    public DddInPracticeDbContext(DbContextOptions<DddInPracticeDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {

        using var transaction = Database.BeginTransaction();

        int result = base.SaveChanges();

        var aggregateRootEntitiesWithEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();

        foreach (var entity in aggregateRootEntitiesWithEvents)
        {
            foreach (var domainEvent in entity.DomainEvents)
            {
                DomainEvents.Dispatch(domainEvent);
            }

            entity.ClearEvents();
        }

        transaction.Commit();
        return result;
    }
}
