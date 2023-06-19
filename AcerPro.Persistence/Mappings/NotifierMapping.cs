using AcerPro.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcerPro.Persistence.Mappings;

internal class NotifierMapping : IEntityTypeConfiguration<Notifier>
{
    public void Configure(EntityTypeBuilder<Notifier> builder)
    {
        builder.ToTable("Notifiers");

        builder.Property(c => c.Address)
            .HasColumnName(nameof(Notifier.Address))
            .HasMaxLength(Notifier.AddressMaxLength)
            .IsUnicode(false)
            .IsRequired();

        builder.HasQueryFilter(c => c.IsDeleted == false);
    }
}
