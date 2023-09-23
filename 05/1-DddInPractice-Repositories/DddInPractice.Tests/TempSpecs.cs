using DddInPractice.Logic;
using static DddInPractice.Logic.Money;

namespace DddInPractice.Tests;

public class TempSpecs
{
    [Fact]
    public void GetSnackMachine()
    {
        var connectionString = "Server=(localdb)\\mssqllocaldb;Database=DddInPractice;Trusted_Connection=True;MultipleActiveResultSets=true";
        DbContextFactory.Init(connectionString);

        var repository = new SnackMachineRepository();
        SnackMachine? snackMachine = repository.GetById(1);
        if (snackMachine is not null)
        {
            snackMachine.InsertMoney(Dollar);
            snackMachine.InsertMoney(Dollar);
            snackMachine.InsertMoney(Dollar);
            snackMachine.BuySnack(1);
            repository.Save(snackMachine);
        }
        
    }
}
