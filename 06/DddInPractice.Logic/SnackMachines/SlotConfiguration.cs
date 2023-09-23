using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddInPractice.Logic.SnackMachines;

public class SlotConfiguration : IEntityTypeConfiguration<Slot>
{
    public void Configure(EntityTypeBuilder<Slot> builder)
    {
        builder.ToTable(nameof(Slot));
        builder.Property(t => t.Id).HasColumnName(nameof(Slot) + "ID");

        builder.OwnsOne(s => s.SnackPile, nav =>
        {
            nav.Property(p => p.Price).HasColumnName(nameof(SnackPile.Price));
            nav.Property(p => p.Quantity).HasColumnName(nameof(SnackPile.Quantity));
        });

        builder.Navigation(t => t.SnackPile).IsRequired();
        builder.Navigation(t => t.Snack).AutoInclude();
    }
}
