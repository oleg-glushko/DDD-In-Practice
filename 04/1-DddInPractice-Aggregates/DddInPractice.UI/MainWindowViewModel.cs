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
        SnackMachine? snackMachine;
        using (var context = DbContextFactory.GetDbContext())
        {
            snackMachine = context.SnackMachines.Find(1L);
        }

        if (snackMachine is null)
            throw new NullReferenceException();

        _snackMachineViewModel = new(snackMachine);
        CurrentViewModel = _snackMachineViewModel;
        Caption = "Snack Machine";
    }
}
