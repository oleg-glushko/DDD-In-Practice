namespace DddInPractice.Logic;

public sealed class SnackPile : ValueObject<SnackPile>
{
    public static readonly SnackPile Empty = new(0, 0m);
    public int Quantity { get; init; }
    public decimal Price { get; init; }

    public SnackPile(int quantity, decimal price)
    {
        if (quantity < 0)
            throw new InvalidOperationException();
        if (price < 0)
            throw new InvalidOperationException();
        if (price % 0.01m > 0)
            throw new InvalidOperationException();

        Quantity = quantity;
        Price = price;
    }

    public SnackPile SubtractOne()
    {
        return new SnackPile(Quantity - 1, Price);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Quantity;
        yield return Price;
    }
}
