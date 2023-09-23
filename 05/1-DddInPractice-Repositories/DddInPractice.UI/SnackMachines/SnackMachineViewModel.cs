using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DddInPractice.Logic;

namespace DddInPractice.UI.SnackMachines;

public partial class SnackMachineViewModel : ObservableObject
{
    private readonly SnackMachine _snackMachine;
    public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString();
    public Money MoneyInside => _snackMachine.MoneyInside;

    [ObservableProperty]
    private string _message = "";

    [RelayCommand]
    private void InsertCent() => InsertMoney(Money.Cent);

    [RelayCommand]
    private void InsertTenCent() => InsertMoney(Money.TenCent);

    [RelayCommand]
    private void InsertQuarter() => InsertMoney(Money.Quarter);

    [RelayCommand]
    private void InsertDollar() => InsertMoney(Money.Dollar);

    [RelayCommand]
    private void InsertFiveDollar() => InsertMoney(Money.FiveDollar);

    [RelayCommand]
    private void InsertTwentyDollar() => InsertMoney(Money.TwentyDollar);


    public SnackMachineViewModel(SnackMachine snackMachine)
    {
        _snackMachine = snackMachine;
    }

    [RelayCommand]
    private void ReturnMoney()
    {
        _snackMachine.ReturnMoney();
        NotifyClient("Money was returned.");
    }

    [RelayCommand]
    private void BuySnack()
    {
        _snackMachine.BuySnack(1);
        using (var context = DbContextFactory.GetDbContext())
        {
            context.Update(_snackMachine);
            context.SaveChanges();
        }
        NotifyClient("You have bought a snack");
    }

    private void InsertMoney(Money coinOrNote)
    {
        _snackMachine.InsertMoney(coinOrNote);
        NotifyClient("You have inserted " + coinOrNote);
    }

    private void NotifyClient(string message)
    {
        Message = message;
        OnPropertyChanged(nameof(MoneyInTransaction));
        OnPropertyChanged(nameof(MoneyInside));
    }
}
