namespace DddInPractice.Logic;

public sealed class SnackPile : ValueObject<SnackPile>
{
    public Snack Snack { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }

    public SnackPile(Snack snack, int quantity, decimal price)
    {
        if (quantity < 0)
            throw new InvalidOperationException();
        if (price < 0)
            throw new InvalidOperationException();
        if (price % 0.01m > 0)
            throw new InvalidOperationException();

        Snack = snack;
        Quantity = quantity;
        Price = price;
    }

    public SnackPile SubtractOne()
    {
        return new SnackPile(Snack, Quantity - 1, Price);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Snack;
        yield return Quantity;
        yield return Price;
    }
}
