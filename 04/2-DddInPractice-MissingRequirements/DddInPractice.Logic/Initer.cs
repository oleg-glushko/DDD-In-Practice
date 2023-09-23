namespace DddInPractice.Logic;

public static class Initer
{
    public static void Init(string connectionString)
    {
        DbContextFactory.Init(connectionString);
    }
}
