using DddInPractice.Logic.Atms;
using DddInPractice.Logic.SharedKernel;
using FluentAssertions;

using static DddInPractice.Logic.SharedKernel.Money;

namespace DddInPractice.Tests;

public class AtmSpecs
{
    [Fact]
    public void TakeMoney_exchanges_money_with_commission()
    {
        var atm = new Atm();
        atm.LoadMoney(Dollar);

        atm.TakeMoney(1m);

        atm.MoneyInside.Amount.Should().Be(0m);
        atm.MoneyCharged.Should().Be(1.01m);
    }

    [Fact]
    public void TakeMoney_apply_at_least_one_cent_commission()
    {
        var atm = new Atm();
        atm.LoadMoney(Cent);

        atm.TakeMoney(0.01m);

        atm.MoneyCharged.Should().Be(0.02m);
    }

    [Fact]
    public void TakeMoney_rounds_commission_up_to_the_next_cent()
    {
        var atm = new Atm();
        atm.LoadMoney(Dollar + TenCent);

        atm.TakeMoney(1.1m);

        atm.MoneyCharged.Should().Be(1.12m);
    }
}
