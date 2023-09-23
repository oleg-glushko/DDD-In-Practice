namespace DddInPractice.Logic;

public class Snack : AggregateRoot
{
    public static readonly Snack None = new("None");

    public string Name { get; private set; }


    public Snack(string name)
    {
        Name = name;
    }
}
