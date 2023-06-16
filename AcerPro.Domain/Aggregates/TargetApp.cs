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

    private readonly List<TargetAppNotifier> _targetAppNotifiers = new();
    public IReadOnlyList<TargetAppNotifier> TargetAppNotifiers => _targetAppNotifiers;
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

    internal Result AddNotifier(string address,NotifierType notifierType)
    {
        var notiferResult = Notifier.Create(address, notifierType);

        if (notiferResult.IsFailed)
            return Result.Fail(notiferResult.Errors);

        if (_targetAppNotifiers.Any(c=> c.Notifier.NotifierType == notifierType))
        {
            var endpointNotifier = _targetAppNotifiers.Where(c => c.Notifier.NotifierType == notifierType).First();
            var updateResult = endpointNotifier.Notifier.Update(address);

            if (updateResult.IsFailed)
                return updateResult;

            return Result.Ok();
        }

        _targetAppNotifiers.Add(new TargetAppNotifier(this,notiferResult.Value));
        return Result.Ok();
    }


}