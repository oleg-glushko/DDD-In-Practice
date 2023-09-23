namespace DddInPractice.Logic;
using static DddInPractice.Logic.Money;

public sealed class SnackMachine : AggregateRoot
{
    public Money MoneyInside { get; private set; } = null!;
    public decimal MoneyInTransaction { get; private set; }
    public List<Slot> Slots { get; init; } = new();

    private SnackMachine()
    {
    }

    public SnackMachine(bool init)
    {
        MoneyInside = None;
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

    public IReadOnlyList<(Snack, SnackPile)> GetAllSnackPiles()
    {
        return Slots
            .OrderBy(x => x.Position)
            .Select(x => (x.Snack, x.SnackPile))
            .ToList();
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

    public string CanBuySnack(int position)
    {
        SnackPile snackPile = GetSnackPile(position);

        if (snackPile.Quantity == 0)
            return "The snack pile is empty";

        if (MoneyInTransaction < snackPile.Price)
            return "Not enough money";

        if (!MoneyInside.CanAllocate(MoneyInTransaction - snackPile.Price))
            return "Not enough change";

        return string.Empty;
    }

    public void BuySnack(int position)
    {
        if (CanBuySnack(position) != string.Empty)
            throw new InvalidOperationException();

        Slot slot = GetSlot(position);
        slot.SnackPile = slot.SnackPile.SubtractOne();

        Money change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);
        MoneyInside -= change;
        MoneyInTransaction = 0;
    }

    public void LoadSnacks(int position, Snack snack, SnackPile snackPile)
    {
        Slot slot = GetSlot(position);
        slot.Snack = snack;
        slot.SnackPile = snackPile;
    }

    public void LoadMoney(Money money)
    {
        MoneyInside += money;
    }
}
