using DddInPractice.Logic.Common;
using DddInPractice.Logic.Utils;

namespace DddInPractice.Logic.SnackMachines;

public class SnackMachineRepository : Repository<SnackMachine>
{
    public SnackMachineDto? GetSnackMachineDto(long id)
    {
        using var context = DbContextFactory.GetDbContext();

        var snackMachine = context.SnackMachines.FirstOrDefault(x => x.Id == id);
        return snackMachine is null ? null : new SnackMachineDto(snackMachine.Id, snackMachine.MoneyInside.Amount);
    }
}
