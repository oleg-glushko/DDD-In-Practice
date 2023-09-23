using System.Globalization;

namespace DddInPractice.Logic;

public class Money : ValueObject<Money>
{
    public static readonly Money None = new(0, 0, 0, 0, 0, 0);
    public static Money Cent => new(1, 0, 0, 0, 0, 0);
    public static Money TenCent => new(0, 1, 0, 0, 0, 0);
    public static Money Quarter => new(0, 0, 1, 0, 0, 0);
    public static Money Dollar => new(0, 0, 0, 1, 0, 0);
    public static Money FiveDollar => new(0, 0, 0, 0, 1, 0);
    public static Money TwentyDollar => new(0, 0, 0, 0, 0, 1);

    public int OneCentCount { get; init; }
    public int TenCentCount { get; init; }
    public int QuarterCount { get; init; }
    public int OneDollarCount { get;  init; }
    public int FiveDollarCount { get; init; }
    public int TwentyDollarCount { get; init; }

    public decimal Amount =>
        OneCentCount * 0.01m +
        TenCentCount * 0.1m +
        QuarterCount * 0.25m +
        OneDollarCount +
        FiveDollarCount * 5 +
        TwentyDollarCount * 20;

    public Money(int oneCentCount, int tenCentCount, int quarterCount,
        int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
    {
        if (oneCentCount < 0)
            throw new InvalidOperationException();
        if (tenCentCount < 0)
            throw new InvalidOperationException();
        if (quarterCount < 0)
            throw new InvalidOperationException();
        if (oneDollarCount < 0)
            throw new InvalidOperationException();
        if (fiveDollarCount < 0)
            throw new InvalidOperationException();
        if (twentyDollarCount < 0)
            throw new InvalidOperationException();

        OneCentCount = oneCentCount;
        TenCentCount = tenCentCount;
        QuarterCount = quarterCount;
        OneDollarCount = oneDollarCount;
        FiveDollarCount = fiveDollarCount;
        TwentyDollarCount = twentyDollarCount;
    }

    public static Money operator +(Money money1, Money money2)
    {
        Money sum = new(
            money1.OneCentCount + money2.OneCentCount,
            money1.TenCentCount + money2.TenCentCount,
            money1.QuarterCount + money2.QuarterCount,
            money1.OneDollarCount + money2.OneDollarCount,
            money1.FiveDollarCount + money2.FiveDollarCount,
            money1.TwentyDollarCount + money2.TwentyDollarCount);

        return sum;
    }

    public static Money operator -(Money money1, Money money2)
    {
        return new Money(
            money1.OneCentCount - money2.OneCentCount,
            money1.TenCentCount - money2.TenCentCount,
            money1.QuarterCount - money2.QuarterCount,
            money1.OneDollarCount - money2.OneDollarCount,
            money1.FiveDollarCount - money2.FiveDollarCount,
            money1.TwentyDollarCount - money2.TwentyDollarCount);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return OneCentCount;
        yield return TenCentCount;
        yield return QuarterCount;
        yield return OneDollarCount;
        yield return FiveDollarCount;
        yield return TwentyDollarCount;
    }

    public override string ToString()
    {
        if (Amount < 1)
            return "¢" + (Amount * 100).ToString("0", CultureInfo.InvariantCulture);

        return "$" + Amount.ToString("0.00", CultureInfo.InvariantCulture);
    }
}
