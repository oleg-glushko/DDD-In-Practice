namespace DddInPractice.Logic;

public class Slot : Entity
{
    public SnackPile SnackPile { get; set; }
    public SnackMachine SnackMachine { get; private set; }
    public int Position { get; private set; }

    public Slot(SnackMachine snackMachine, int position)
    {
        SnackMachine = snackMachine;
        Position = position;
        SnackPile = new(Snack.None, 0, 0m);
    }
}
