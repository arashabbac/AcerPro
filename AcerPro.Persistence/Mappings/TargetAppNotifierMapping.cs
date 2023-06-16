using AcerPro.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcerPro.Persistence.Mappings;

internal class TargetAppNotifierMapping : IEntityTypeConfiguration<TargetAppNotifier>
{
    public void Configure(EntityTypeBuilder<TargetAppNotifier> builder)
    {
        builder
            .HasKey(pc => new { pc.TargetAppId, pc.NotifierId });

        builder
            .HasOne(ta => ta.TargetApp)
            .WithMany(t => t.TargetAppNotifiers)
            .HasForeignKey(tn => tn.TargetAppId)
            .OnDelete(DeleteBehavior.Restrict)
            ;

        builder
            .HasOne(ta => ta.Notifier)
            .WithMany(n => n.TargetAppNotifiers)
            .HasForeignKey(tn => tn.NotifierId)
            .OnDelete(DeleteBehavior.Restrict)
            ;
    }
}
