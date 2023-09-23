namespace DddInPractice.Logic.Utils;

public static class Initer
{
    public static void Init(string connectionString)
    {
        DbContextFactory.Init(connectionString);
    }
}
