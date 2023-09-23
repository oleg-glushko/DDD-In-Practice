using DddInPractice.Logic;
using FluentAssertions;

namespace DddInPractice.Tests;

public class SnackPileSpecs
{
    [Fact]
    public void Can_not_create_SnackPile_with_negative_quantity()
    {
        Action action = () => {
            var snack = new SnackPile(Snack.None, -1, 0);
        };

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Can_not_create_SnackPile_with_negative_price()
    {
        Action action = () => {
            var snack = new SnackPile(Snack.None, 0, -1m);
        };

        action.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData(0.001)]
    [InlineData(1.2345)]
    [InlineData(9.99999)]
    public void Price_precision_could_not_be_less_than_one_cent(decimal price)
    {
        
        Action action = () => {
            var snack = new SnackPile(Snack.None, 0, price);
        };
        
        action.Should().Throw<InvalidOperationException>();
    }
}