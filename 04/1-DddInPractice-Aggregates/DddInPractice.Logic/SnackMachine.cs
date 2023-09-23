namespace DddInPractice.Logic;
using static DddInPractice.Logic.Money;

public sealed class SnackMachine : AggregateRoot
{
    public Money MoneyInside { get; private set; }
    public Money MoneyInTransaction { get; private set; }
    private List<Slot> Slots { get; init; }

    public SnackMachine()
    {
        MoneyInside = None;
        MoneyInTransaction = None;
        Slots = new List<Slot>()
        {
            new Slot(this, 1),
            new Slot(this, 2),
            new Slot(this, 3),
        };
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

    public void LoadSnacks(int position, SnackPile snackPile)
    {
        Slot slot = GetSlot(position);
        slot.SnackPile = snackPile;
    }
}
