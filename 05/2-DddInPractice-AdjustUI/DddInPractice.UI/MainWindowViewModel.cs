using CommunityToolkit.Mvvm.ComponentModel;
using DddInPractice.Logic;
using System;

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
        SnackMachine snackMachine = new SnackMachineRepository().GetById(1)
            ?? throw new NullReferenceException();
        _snackMachineViewModel = new(snackMachine);
        CurrentViewModel = _snackMachineViewModel;
        Caption = "Snack Machine";
    }
}
