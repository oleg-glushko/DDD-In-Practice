using DddInPractice.Logic.Common;
using DddInPractice.Logic.Utils;

namespace DddInPractice.Logic.Atms;

public class AtmRepository : Repository<Atm>
{
    public AtmDto? GetAtmDto(long id)
    {
        using var context = DbContextFactory.GetDbContext();

        var atm = context.Atms.FirstOrDefault(x => x.Id == id);
        return atm is null ? null : new AtmDto(atm.Id, atm.MoneyInside.Amount);
    }
}
