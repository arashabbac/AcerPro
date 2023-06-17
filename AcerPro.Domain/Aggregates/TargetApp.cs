using AcerPro.Domain.Enums;
using AcerPro.Domain.ValueObjects;
using FluentResults;
using Framework.Domain;

namespace AcerPro.Domain.Aggregates;

public class TargetApp : Entity<int>
{
    #region Static Member(s)
    public static Result<TargetApp> Create(Name name,
        UrlAddress urlAddress,
        int monitoringIntervalInSeconds,
        int? userId)
    {
        ArgumentNullException.ThrowIfNull(urlAddress, nameof(urlAddress));
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));

        if(monitoringIntervalInSeconds == default || monitoringIntervalInSeconds < 0)
            return Result.Fail<TargetApp>($"{nameof(monitoringIntervalInSeconds)} must be specified");

        return Result.Ok(new TargetApp(name, urlAddress, monitoringIntervalInSeconds,userId.Value));
    }
    #endregion

    //For EF Core
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TargetApp() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TargetApp(Name name,
        UrlAddress urlAddress,
        int monitoringIntervalInSeconds,
        int userId)
    {
        Name = name;
        UrlAddress = urlAddress;
        MonitoringIntervalInSeconds = monitoringIntervalInSeconds;
        UserId = userId;
    }

    public Name Name { get; private set; }
    public UrlAddress UrlAddress { get; private set; }
    public int MonitoringIntervalInSeconds { get; private set; }
    public DateTime LastDownDateTime { get; private set; }
    public bool IsHealthy { get; private set; }

    private readonly List<Notifier> _notifiers = new();
    public IReadOnlyList<Notifier> Notifiers => _notifiers;
    public int UserId { get; private set; }
    public User? User { get; private set; }

    public Result<TargetApp> Update(Name name,
        UrlAddress urlAddress,
        int monitoringIntervalInSeconds)
    {
        ArgumentNullException.ThrowIfNull(urlAddress, nameof(urlAddress));
        ArgumentNullException.ThrowIfNull(name, nameof(name));

        if (monitoringIntervalInSeconds == default || monitoringIntervalInSeconds < 0)
            return Result.Fail<TargetApp>($"{nameof(monitoringIntervalInSeconds)} must be specified");

        Name = name;
        UrlAddress = urlAddress;
        MonitoringIntervalInSeconds = monitoringIntervalInSeconds;

        return Result.Ok(this);
    }

    internal Result<Notifier> AddNotifier(string address,NotifierType notifierType)
    {
        var notiferResult = Notifier.Create(Id,address, notifierType);

        if (notiferResult.IsFailed)
            return Result.Fail(notiferResult.Errors);

        if (_notifiers.Any(c=> c.NotifierType == notifierType))
        {
            var notifier = _notifiers.Where(c => c.NotifierType == notifierType).First();
            var updateResult = notifier.Update(address);

            if (updateResult.IsFailed)
                return updateResult;

            return Result.Ok(updateResult.Value);
        }

        _notifiers.Add(notiferResult.Value);
        return Result.Ok(notiferResult.Value);
    }


}