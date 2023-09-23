using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddInPractice.Logic;

public class SnackConfiguration : IEntityTypeConfiguration<Snack>
{
    public void Configure(EntityTypeBuilder<Snack> builder)
    {
        builder.ToTable(nameof(Snack));
        builder.Property(t => t.Id).HasColumnName(nameof(Snack) + "ID");
    }
}
