using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection;

namespace DddInPractice.Logic;
public class DddInPracticeDbContext : DbContext
{
    public DbSet<SnackMachine> SnackMachines { get; set; } = null!;

    public DddInPracticeDbContext(DbContextOptions<DddInPracticeDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
