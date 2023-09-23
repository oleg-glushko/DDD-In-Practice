﻿using DddInPractice.Logic;
using FluentAssertions;
using static DddInPractice.Logic.Money;

namespace DddInPractice.Tests;

public class SnackMachineSpecs
{
    [Fact]
    public void Return_money_empties_money_in_transaction()
    {
        var snackMachine = new SnackMachine(true);
        snackMachine.InsertMoney(Dollar);

        snackMachine.ReturnMoney();

        snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
    }

    [Fact]
    public void Inserted_money_goes_to_money_in_transaction()
    {
        var snackMachine = new SnackMachine(true);
        snackMachine.InsertMoney(Cent);
        snackMachine.InsertMoney(Dollar);

        snackMachine.MoneyInTransaction.Amount.Should().Be(1.01m);
    }

    [Fact]
    public void Cannot_insert_more_than_one_coin_or_note_at_time()
    {
        var snackMachine = new SnackMachine(true);
        var twoCent = Cent + Cent;
        
        Action action = () => snackMachine.InsertMoney(twoCent);

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void BuySnack_trades_inserted_money_for_a_snack()
    {
        var snackMachine = new SnackMachine(true);
        snackMachine.LoadSnacks(1, new Snack("Some snack"), new SnackPile(10, 1m));
        snackMachine.InsertMoney(Dollar);

        snackMachine.BuySnack(1);

        snackMachine.MoneyInTransaction.Should().Be(None);
        snackMachine.MoneyInside.Amount.Should().Be(1m);
        snackMachine.GetSnackPile(1).Quantity.Should().Be(9);
    }
}
