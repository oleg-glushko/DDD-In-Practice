using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DddInPractice.Logic.Atms;
using DddInPractice.Logic.SharedKernel;
using System.Globalization;

namespace DddInPractice.UI.Atms;

public partial class AtmViewModel : ObservableObject
{
    private readonly PaymentGateway _paymentGateway;
    private readonly AtmRepository _repository;
    private readonly Atm _atm;

    [ObservableProperty]
    private string _message = string.Empty;

    public string Caption => "ATM";
    public Money MoneyInside => _atm.MoneyInside;
    public string MoneyCharged => _atm.MoneyCharged.ToString("C2", CultureInfo.GetCultureInfo("en-US"));

    public AtmViewModel(Atm atm)
    {
        _atm = atm;
        _repository = new AtmRepository();
        _paymentGateway = new PaymentGateway();
    }

    [RelayCommand]
    public void TakeMoney(decimal amount)
    {
        string error = _atm.CanTakeMoney(amount);
        if (error != string.Empty)
        {
            NotifyClient(error);
            return;
        }

        decimal amountWithCommission = _atm.CalculateAmountWithCommission(amount);
        _paymentGateway.ChargePayment(amountWithCommission);
        _atm.TakeMoney(amount);
        _repository.Save(_atm);

        NotifyClient("You have taken " + amount.ToString("C2", CultureInfo.GetCultureInfo("en-US")));
    }

    private void NotifyClient(string message)
    {
        Message = message;
        OnPropertyChanged(nameof(MoneyInside));
        OnPropertyChanged(nameof(MoneyCharged));
    }
}
