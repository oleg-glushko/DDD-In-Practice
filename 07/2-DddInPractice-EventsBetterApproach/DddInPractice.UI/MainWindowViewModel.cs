using CommunityToolkit.Mvvm.ComponentModel;
using DddInPractice.Logic.SnackMachines;
using DddInPractice.UI.SnackMachines;
using DddInPractice.UI.Atms;
using System;
using DddInPractice.Logic.Atms;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows;
using DddInPractice.UI.Management;

namespace DddInPractice.UI;

public partial class MainWindowViewModel : ObservableObject
{
    private SnackMachineViewModel _snackMachineViewModel = null!;
    private AtmViewModel _atmViewModel = null!;
    private readonly DashboardViewModel _dashboardViewModel = null!;

    [ObservableProperty]
    private string _caption = "DDD In Practice";

    [ObservableProperty]
    private ObservableObject? _currentViewModel;

    [RelayCommand]
    public void ShowSnackMachineView()
    {
        SnackMachine snackMachine = new SnackMachineRepository().GetById(1)
            ?? throw new NullReferenceException();
        _snackMachineViewModel = new(snackMachine);

        CurrentViewModel = _snackMachineViewModel;
        Caption = _snackMachineViewModel.Caption;
    }

    [RelayCommand]
    public void ShowAtmView()
    {
        Atm atm = new AtmRepository().GetById(1)
            ?? throw new NullReferenceException();
        _atmViewModel = new AtmViewModel(atm);

        CurrentViewModel = _atmViewModel;
        Caption = _atmViewModel.Caption;
    }

    [RelayCommand]
    public void ShowDashboardView()
    {
        _dashboardViewModel.RefreshAll();
        CurrentViewModel = _dashboardViewModel;
        Caption = _dashboardViewModel.Caption;
    }

    public MainWindowViewModel()
    {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            return;

        _dashboardViewModel = new DashboardViewModel();
        CurrentViewModel = _dashboardViewModel;
        Caption = _dashboardViewModel.Caption;
    }
}
