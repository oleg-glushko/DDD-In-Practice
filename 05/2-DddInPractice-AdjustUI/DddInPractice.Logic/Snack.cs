namespace DddInPractice.Logic;

public class Snack : AggregateRoot
{
    public static readonly Snack None = new(0, "None");
    public static readonly Snack Chocolate = new(1, "Chocolate");
    public static readonly Snack Soda = new(2, "Soda");
    public static readonly Snack Gum = new(3, "Gum");

    public string Name { get; private set; }


    public Snack(long id, string name)
    {
        Id = id;
        Name = name;
    }
}
