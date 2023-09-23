using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DddInPractice.Logic;
using System.Collections.Generic;
using System.Linq;

namespace DddInPractice.UI.SnackMachines;

public partial class SnackMachineViewModel : ObservableObject
{
    private readonly SnackMachine _snackMachine;
    private readonly SnackMachineRepository _repository;

    public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString();
    public Money MoneyInside => _snackMachine.MoneyInside;

    public IReadOnlyList<SnackPileViewModel> Piles =>
        _snackMachine.GetAllSnackPiles()
            .Select(x => new SnackPileViewModel(x))    
            .ToList();

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
        _repository = new SnackMachineRepository();
    }

    [RelayCommand]
    private void ReturnMoney()
    {
        _snackMachine.ReturnMoney();
        NotifyClient("Money was returned.");
    }

    [RelayCommand]
    private void BuySnack(string positionString)
    {
        int position = int.Parse(positionString);

        string error = _snackMachine.CanBuySnack(position);
        if (error != string.Empty)
        {
            NotifyClient(error);
            return;
        }

        _snackMachine.BuySnack(position);
        _repository.Save(_snackMachine);
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
        OnPropertyChanged(nameof(Piles));
    }
}
