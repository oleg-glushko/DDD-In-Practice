using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DddInPractice.Logic.Atms;
using DddInPractice.Logic.Management;
using DddInPractice.Logic.SnackMachines;
using System;
using System.Collections.Generic;

namespace DddInPractice.UI.Management;

public partial class DashboardViewModel : ObservableObject
{
    private readonly SnackMachineRepository _snackMachineRepository;
    private readonly AtmRepository _atmRepository;
    private readonly HeadOfficeRepository _headOfficeRepository;

    public string Caption => "Management Dashboard";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoadCashToAtmCommand))]
    private AtmDto? _selectedAtm;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UnloadCashCommand))]
    private SnackMachineDto? _selectedSnackMachine;

    public HeadOffice HeadOffice { get; }

    public DashboardViewModel()
    {
        HeadOffice = HeadOfficeInstance.Instance;
        _snackMachineRepository = new SnackMachineRepository();
        _atmRepository = new AtmRepository();
        _headOfficeRepository = new HeadOfficeRepository();

        RefreshAll();
    }


    [RelayCommand(CanExecute = nameof(CanUnloadCash))]
    private void UnloadCash()
    {
        SnackMachine? snackMachine = _snackMachineRepository.GetById(SelectedSnackMachine!.Id);

        if (snackMachine is null)
            return;

        HeadOffice.UnloadCashFromSnackMachine(snackMachine);
        _snackMachineRepository.Save(snackMachine);
        _headOfficeRepository.Save(HeadOffice);

        RefreshAll();
    }

    private bool CanUnloadCash() => SelectedSnackMachine?.MoneyInside > 0;


    [RelayCommand(CanExecute = nameof(CanLoadCashToAtm))]
    private void LoadCashToAtm()
    {
        Atm? atm = _atmRepository.GetById(SelectedAtm!.Id);

        if (atm is null)
            return;

        HeadOffice.LoadCashToAtm(atm);
        _atmRepository.Save(atm);
        _headOfficeRepository.Save(HeadOffice);

        RefreshAll();
    }

    private bool CanLoadCashToAtm() => HeadOffice.Cash.Amount > 0;


    public void RefreshAll()
    {
        SelectedSnackMachine = _snackMachineRepository.GetSnackMachineDto(1)
            ?? throw new NullReferenceException();
        SelectedAtm = _atmRepository.GetAtmDto(1)
            ?? throw new NullReferenceException();

        OnPropertyChanged(nameof(HeadOffice));
    }
}
