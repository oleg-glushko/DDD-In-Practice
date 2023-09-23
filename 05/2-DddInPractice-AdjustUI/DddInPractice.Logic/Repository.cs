namespace DddInPractice.Logic;

public abstract class Repository<T> where T : AggregateRoot
{
    public T? GetById(long id)
    {
        using var context = DbContextFactory.GetDbContext();
        return context.Find<T>(id);
    }

    public void Save(T aggregateRoot)
    {
        using var context = DbContextFactory.GetDbContext();
        context.Update(aggregateRoot);
        context.SaveChanges();
    }
}
