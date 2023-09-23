using DddInPractice.Logic.Common;
using DddInPractice.Logic.SharedKernel;

namespace DddInPractice.Logic.Management;

public class HeadOffice : AggregateRoot
{
    public decimal Balance { get; private set; }
    public Money Cash { get; private set; } = Money.None;

    public void ChangeBalance(decimal delta)
    {
        Balance += delta;
    }
}
