namespace DddInPractice.Logic;
using static DddInPractice.Logic.Money;

public sealed class SnackMachine : AggregateRoot
{
    public Money MoneyInside { get; private set; }
    public decimal MoneyInTransaction { get; private set; }
    private List<Slot> Slots { get; init; }

    public SnackMachine()
    {
        MoneyInside = None;
        MoneyInTransaction = 0;
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
        MoneyInTransaction += money.Amount;
        MoneyInside += money;
    }

    public void ReturnMoney()
    {
        Money moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
        MoneyInside -= moneyToReturn;
        MoneyInTransaction = 0;
    }

    public void BuySnack(int position)
    {
        Slot slot = GetSlot(position);
        if (slot.SnackPile.Price > MoneyInTransaction)
            throw new InvalidOperationException();

        slot.SnackPile = slot.SnackPile.SubtractOne();

        Money change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);
        if (change.Amount < MoneyInTransaction - slot.SnackPile.Price)
            throw new InvalidOperationException();

        MoneyInside -= change;
        MoneyInTransaction = 0;
    }

    public void LoadSnacks(int position, SnackPile snackPile)
    {
        Slot slot = GetSlot(position);
        slot.SnackPile = snackPile;
    }

    public void LoadMoney(Money money)
    {
        MoneyInside += money;
    }
}
