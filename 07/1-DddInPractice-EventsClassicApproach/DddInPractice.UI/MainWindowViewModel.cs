using CommunityToolkit.Mvvm.ComponentModel;
using DddInPractice.Logic.SnackMachines;
using DddInPractice.UI.SnackMachines;
using DddInPractice.UI.Atms;
using System;
using DddInPractice.Logic.Atms;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows;

namespace DddInPractice.UI;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly SnackMachineViewModel _snackMachineViewModel = null!;
    private readonly AtmViewModel _atmViewModel = null!;

    [ObservableProperty]
    private string _caption = "DDD In Practice";

    [ObservableProperty]
    private ObservableObject? _currentViewModel;

    [RelayCommand]
    public void ShowSnackMachineView()
    {
        CurrentViewModel = _snackMachineViewModel;
        Caption = _snackMachineViewModel.Caption;
    }

    [RelayCommand]
    public void ShowAtmView()
    {
        CurrentViewModel = _atmViewModel;
        Caption = _atmViewModel.Caption;
    }

    public MainWindowViewModel()
    {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            return;

        SnackMachine snackMachine = new SnackMachineRepository().GetById(1)
            ?? throw new NullReferenceException();
        _snackMachineViewModel = new(snackMachine);

        Atm atm = new AtmRepository().GetById(1)
            ?? throw new NullReferenceException();
        _atmViewModel = new AtmViewModel(atm);

        CurrentViewModel = _snackMachineViewModel;
        Caption = _snackMachineViewModel.Caption;
    }
}
