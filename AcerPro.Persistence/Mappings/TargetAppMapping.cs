using AcerPro.Domain.Aggregates;
using AcerPro.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcerPro.Persistence.Mappings;

internal class TargetAppMapping : IEntityTypeConfiguration<TargetApp>
{
    public void Configure(EntityTypeBuilder<TargetApp> builder)
    {
        builder.ToTable("TargetApps");

        builder.Property(c => c.Name)
            .HasColumnName(nameof(TargetApp.Name))
            .HasMaxLength(Name.MaxLenght)
            .IsUnicode(false)
            .IsRequired()
            .HasConversion(c => c.Value, c => Name.Create(c).Value);

        builder.Property(c => c.UrlAddress)
            .HasColumnName(nameof(TargetApp.UrlAddress))
            .HasMaxLength(UrlAddress.MaxLenght)
            .IsUnicode(false)
            .IsRequired()
            .HasConversion(c => c.Value, c => UrlAddress.Create(c).Value);

        builder.HasMany(c=> c.Notifiers)
            .WithOne(c=> c.TargetApp)
            .HasForeignKey(c=>c.TargetAppId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
