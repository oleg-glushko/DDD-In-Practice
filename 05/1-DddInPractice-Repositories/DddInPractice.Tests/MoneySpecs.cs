using DddInPractice.Logic;
using FluentAssertions;

using static DddInPractice.Logic.Money;

namespace DddInPractice.Tests;

public class MoneySpecs
{
    [Fact]
    public void Sum_of_two_moneys_produces_correct_result()
    {
        Money money1 = new(1, 2, 3, 4, 5, 6);
        Money money2 = new(1, 2, 3, 4, 5, 6);

        Money sum = money1 + money2;

        sum.OneCentCount.Should().Be(2);
        sum.TenCentCount.Should().Be(4);
        sum.QuarterCount.Should().Be(6);
        sum.OneDollarCount.Should().Be(8);
        sum.FiveDollarCount.Should().Be(10);
        sum.TwentyDollarCount.Should().Be(12);
    }

    [Fact]
    public void Two_money_instances_equal_if_contain_same_money_amount()
    {
        Money money1 = new(1, 2, 3, 4, 5, 6);
        Money money2 = new(1, 2, 3, 4, 5, 6);

        money1.Should().Be(money2);
    }

    [Fact]
    public void Two_money_instances_do_not_equal_if_contain_different_money_amounts()
    {
        Money dollar = new(0, 0, 0, 1, 0, 0);
        Money hundredCents = new(100, 0, 0, 0, 0, 0);

        dollar.Should().NotBe(hundredCents);
    }

    [Theory]
    [InlineData(-1, 0, 0, 0, 0, 0)]
    [InlineData(0, -2, 0, 0, 0, 0)]
    [InlineData(0, 0, -3, 0, 0, 0)]
    [InlineData(0, 0, 0, -4, 0, 0)]
    [InlineData(0, 0, 0, 0, -5, 0)]
    [InlineData(0, 0, 0, 0, 0, -6)]
    public void Cannot_create_money_with_negative_value(
        int oneCentCount, int tenCentCount, int quarterCount,
        int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
    {
        Action action = () =>
        {
            Money money = new(oneCentCount, tenCentCount, quarterCount,
            oneDollarCount, fiveDollarCount, twentyDollarCount);
        };

        action.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData(0, 0, 0, 0, 0, 0, 0)]
    [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
    [InlineData(1, 2, 0, 0, 0, 0, 0.21)]
    [InlineData(1, 2, 3, 0, 0, 0, 0.96)]
    [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
    [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
    [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
    [InlineData(11, 0, 0, 0, 0, 0, 0.11)]
    [InlineData(110, 0, 0, 0, 100, 0, 501.1)]
    public void Amount_is_calculated_correctly(
        int oneCentCount, int tenCentCount, int quarterCount,
        int oneDollarCount, int fiveDollarCount, int twentyDollarCount,
        decimal expectedAmount)
    {
        Money money = new(oneCentCount, tenCentCount, quarterCount,
            oneDollarCount, fiveDollarCount, twentyDollarCount);

        money.Amount.Should().Be(expectedAmount);
    }

    [Fact]
    public void Substraction_of_two_moneys_produces_correct_result()
    {
        Money money1 = new(10, 10, 10, 10, 10, 10);
        Money money2 = new(1, 2, 3, 4, 5, 6);

        Money result = money1 - money2;

        result.OneCentCount.Should().Be(9);
        result.TenCentCount.Should().Be(8);
        result.QuarterCount.Should().Be(7);
        result.OneDollarCount.Should().Be(6);
        result.FiveDollarCount.Should().Be(5);
        result.TwentyDollarCount.Should().Be(4);
    }

    [Fact]
    public void Cannot_substract_more_than_exists()
    {
        Money money1 = new(0, 1, 0, 0, 0, 0);
        Money money2 = new(1, 0, 0, 0, 0, 0);

        Action action = () =>
        {
            Money money = money1 - money2;
        };

        action.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData(1, 0, 0, 0, 0, 0, "¢1")]
    [InlineData(0, 0, 0, 1, 0, 0, "$1.00")]
    [InlineData(1, 0, 0, 1, 0, 0, "$1.01")]
    [InlineData(0, 0, 2, 1, 0, 0, "$1.50")]
    public void To_string_returns_correct_string_representation(int oneCentCount, int tenCentCount, int quarterCount,
        int oneDollarCount, int fiveDollarCount, int twentyDollarCount,
        string expectedString)
    {
        Money money = new(oneCentCount, tenCentCount, quarterCount,
            oneDollarCount, fiveDollarCount, twentyDollarCount);

        money.ToString().Should().Be(expectedString);
    }

    [Theory]
    [MemberData(nameof(AllocateTestData))]
    public void Money_should_allocate_largest_possible_nominals(Money deposit, decimal withdraw, Money balance)
    {
        var result = deposit - deposit.Allocate(withdraw);

        result.Should().Be(balance);
    }

    public static IEnumerable<object[]> AllocateTestData()
    {
        yield return new object[] {
            new Money(0, 0, 4, 1, 0, 0),
            1m,
            new Money(0, 0, 4, 0, 0, 0)
        };

        yield return new object[]
        {
            new Money(123, 0, 5, 0, 0, 0),
            1.23m,
            new Money(100, 0, 1, 0, 0, 0)
        };

        yield return new object[]
        {
            new Money(300, 30, 0, 5, 7, 1),
            39.4m,
            new Money(300, 26, 0, 1, 4, 0)
        };
    }
}
