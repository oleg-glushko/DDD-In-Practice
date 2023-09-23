namespace DddInPractice.Logic;
using static DddInPractice.Logic.Money;

public sealed class SnackMachine : AggregateRoot
{
    public Money MoneyInside { get; private set; } = None;
    public Money MoneyInTransaction { get; private set; } = None;
    public List<Slot> Slots { get; init; } = new();

    private SnackMachine()
    {
    }

    public SnackMachine(bool init)
    {
        MoneyInside = None;
        MoneyInTransaction = None;
        if (init)
        {
            Slots.Add(new Slot(this, 1));
            Slots.Add(new Slot(this, 2));
            Slots.Add(new Slot(this, 3));
        }
    }

    public SnackPile GetSnackPile(int position)
    {
        return GetSlot(position).SnackPile;
    }

    public Slot GetSlot(int position) {
        return Slots.Single(x => x.Position == position);
    }


    public void InsertMoney(Money money)
    {
        Money[] coinsAndNotes = { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
        if (!coinsAndNotes.Contains(money))
            throw new InvalidOperationException();
        MoneyInTransaction += money;
    }

    public void ReturnMoney()
    {
        MoneyInTransaction = None;
    }

    public void BuySnack(int position)
    {
        Slot slot = GetSlot(position);
        slot.SnackPile = slot.SnackPile.SubtractOne();

        MoneyInside += MoneyInTransaction;

        MoneyInTransaction = None;
    }

    public void LoadSnacks(int position, Snack snack, SnackPile snackPile)
    {
        Slot slot = GetSlot(position);
        slot.Snack = snack;
        slot.SnackPile = snackPile;
    }
}
