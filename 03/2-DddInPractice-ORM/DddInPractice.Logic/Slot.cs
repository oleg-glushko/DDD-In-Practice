namespace DddInPractice.Logic;

public class Slot : Entity
{
    // EF Core doesn't support full entities in Owned Type fields
    public Snack Snack { get; internal set; } = null!;
    public SnackPile SnackPile { get; internal set; } = null!;
    public SnackMachine SnackMachine { get; private set; } = null!;
    public int Position { get; private set; }

    private Slot()
    {
    }

    public Slot(SnackMachine snackMachine, int position)
    {
        SnackMachine = snackMachine;
        Position = position;
        Snack = new Snack("None");
        SnackPile = new(0, 0m);
    }
}
