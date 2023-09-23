using DddInPractice.Logic.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddInPractice.Logic.Management;

public class HeadOfficeConfiguration : IEntityTypeConfiguration<HeadOffice>
{
    public void Configure(EntityTypeBuilder<HeadOffice> builder)
    {
        builder.ToTable(nameof(HeadOffice));
        builder.Property(t => t.Id).HasColumnName(nameof(HeadOffice) + "ID");

        builder.OwnsOne(sm => sm.Cash, nav =>
        {
            nav.Property(p => p.OneCentCount).HasColumnName(nameof(Money.OneCentCount));
            nav.Property(p => p.TenCentCount).HasColumnName(nameof(Money.TenCentCount));
            nav.Property(p => p.QuarterCount).HasColumnName(nameof(Money.QuarterCount));
            nav.Property(p => p.OneDollarCount).HasColumnName(nameof(Money.OneDollarCount));
            nav.Property(p => p.FiveDollarCount).HasColumnName(nameof(Money.FiveDollarCount));
            nav.Property(p => p.TwentyDollarCount).HasColumnName(nameof(Money.TwentyDollarCount));
        });

        builder.Navigation(t => t.Cash).IsRequired();
    }
}
