namespace DddInPractice.Logic;

public class Slot : Entity
{
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
        Snack = Snack.None;
        SnackPile = SnackPile.Empty;
    }
}
