using DddInPractice.Logic.Atms;
using DddInPractice.Logic.Common;
using DddInPractice.Logic.SharedKernel;
using DddInPractice.Logic.SnackMachines;

namespace DddInPractice.Logic.Management;

public class HeadOffice : AggregateRoot
{
    public decimal Balance { get; private set; }
    public Money Cash { get; private set; } = Money.None;

    public void ChangeBalance(decimal delta)
    {
        Balance += delta;
    }

    public void UnloadCashFromSnackMachine(SnackMachine snackMachine)
    {
        Money money = snackMachine.UnloadMoney();
        Cash += money;
    }

    public void LoadCashToAtm(Atm atm)
    {
        atm.LoadMoney(Cash);
        Cash = Money.None;
    }
}
