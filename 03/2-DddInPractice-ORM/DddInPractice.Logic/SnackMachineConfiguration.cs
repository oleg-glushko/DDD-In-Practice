using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddInPractice.Logic;

public class SnackMachineConfiguration : IEntityTypeConfiguration<SnackMachine>
{
    public void Configure(EntityTypeBuilder<SnackMachine> builder)
    {
        builder.ToTable(nameof(SnackMachine));
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName(nameof(SnackMachine) + "ID").ValueGeneratedOnAdd();

        builder.OwnsOne(sm => sm.MoneyInside, nav =>
        {
            nav.Property(p => p.OneCentCount).HasColumnName(nameof(Money.OneCentCount));
            nav.Property(p => p.TenCentCount).HasColumnName(nameof(Money.TenCentCount));
            nav.Property(p => p.QuarterCount).HasColumnName(nameof(Money.QuarterCount));
            nav.Property(p => p.OneDollarCount).HasColumnName(nameof(Money.OneDollarCount));
            nav.Property(p => p.FiveDollarCount).HasColumnName(nameof(Money.FiveDollarCount));
            nav.Property(p => p.TwentyDollarCount).HasColumnName(nameof(Money.TwentyDollarCount));

            nav.Ignore(p => p.Amount);
        });
            
        builder.Navigation(t => t.MoneyInside).IsRequired();

        builder.Ignore(sm => sm.MoneyInTransaction);
    }
}
