using DddInPractice.Logic.Common;
using DddInPractice.Logic.SharedKernel;

using static DddInPractice.Logic.SharedKernel.Money;

namespace DddInPractice.Logic.Atms;

public class Atm : AggregateRoot
{
    private const decimal CommissionRate = 0.01m;

    public Money MoneyInside { get; private set; } = None;
    public decimal MoneyCharged { get; set; }

    public string CanTakeMoney(decimal amount)
    {
        if (amount <= 0m)
            return "Invalid amount";

        if (MoneyInside.Amount < amount)
            return "Not enough money";

        if (!MoneyInside.CanAllocate(amount))
            return "Not enough change";

        return string.Empty;
    }

    public void TakeMoney(decimal amount)
    {
        if (CanTakeMoney(amount) != string.Empty)
            throw new InvalidOperationException();

        Money output = MoneyInside.Allocate(amount);
        MoneyInside -= output;

        decimal amountWithCommission = CalculateAmountWithCommission(amount);
        MoneyCharged += amountWithCommission;
    }

    public decimal CalculateAmountWithCommission(decimal amount)
    {
        decimal commission = amount * CommissionRate;
        decimal lessThanCent = decimal.Remainder(commission, 0.01m);
        if (lessThanCent > 0)
            commission = commission - lessThanCent + 0.01m;
        return amount + commission;
    }

    public void LoadMoney(Money money)
    {
        MoneyInside += money;
    }
}
