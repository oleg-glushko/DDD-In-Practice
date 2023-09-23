using CommunityToolkit.Mvvm.ComponentModel;
using DddInPractice.Logic;

namespace DddInPractice.UI;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly SnackMachines.SnackMachineViewModel _snackMachineViewModel;

    [ObservableProperty]
    private string _caption;

    [ObservableProperty]
    private ObservableObject? _currentViewModel;

    public MainWindowViewModel()
    {
        _snackMachineViewModel = new(new SnackMachine());
        CurrentViewModel = _snackMachineViewModel;
        Caption = "Snack Machine";
    }

}
