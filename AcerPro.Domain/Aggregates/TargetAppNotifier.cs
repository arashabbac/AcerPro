namespace AcerPro.Domain.Aggregates;

public class TargetAppNotifier
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TargetAppNotifier() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public TargetAppNotifier(TargetApp targetApp, Notifier notifier)
    {
        TargetApp = targetApp;
        Notifier = notifier;
    }

    public int TargetAppId { get; private set; }
    public TargetApp TargetApp { get; private set; }

    public int NotifierId { get; private set; }
    public Notifier Notifier { get; private set; }
}
