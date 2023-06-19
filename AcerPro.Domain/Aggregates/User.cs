using AcerPro.Domain.Aggregates.Specifications;
using AcerPro.Domain.Contracts;
using AcerPro.Domain.Enums;
using AcerPro.Domain.ValueObjects;
using FluentResults;
using Framework.Domain;
namespace AcerPro.Domain.Aggregates;

public sealed class User : AggregateRoot<int>
{
    #region Static Member(s)
    public static Result<User> Create(Name firstname,
        Name lastname,
        Email email,
        Password password,
        IUserRepository userRepository)
    {
        ArgumentNullException.ThrowIfNull(email, nameof(email));
        ArgumentNullException.ThrowIfNull(lastname, nameof(lastname));
        ArgumentNullException.ThrowIfNull(firstname, nameof(firstname));
        ArgumentNullException.ThrowIfNull(password, nameof(password));
        ArgumentNullException.ThrowIfNull(userRepository, nameof(userRepository));

        var isEmailAlreadyUsed = userRepository
            .IsEmailAlreadyUsed(new IsEmailAlreadyUsedSpecification(email.Value));

        if (isEmailAlreadyUsed)
            return Result.Fail<User>("Email has already been used");

        return Result.Ok(new User(firstname, lastname, email,password));
    }
    #endregion

    //For EF Core
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private User() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private User(Name firstname,
        Name lastname,
        Email email,
        Password password)
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Password = password;
    }

    public Name Firstname { get; private set; }
    public Name Lastname { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    private readonly List<TargetApp> _targetApps = new();
    public IReadOnlyList<TargetApp> TargetApps => _targetApps;

    public Result Update(Name firstname,
        Name lastname,
        Email email,
        Password password,
        IUserRepository userRepository)
    {
        ArgumentNullException.ThrowIfNull(email, nameof(email));
        ArgumentNullException.ThrowIfNull(lastname, nameof(lastname));
        ArgumentNullException.ThrowIfNull(firstname, nameof(firstname));
        ArgumentNullException.ThrowIfNull(password, nameof(password));
        ArgumentNullException.ThrowIfNull(userRepository, nameof(userRepository));

        if (IsEmailModified(email))
        {
            var isEmailAlreadyUsed = userRepository
                .IsEmailAlreadyUsed(new IsEmailAlreadyUsedSpecification(email.Value));

            if (isEmailAlreadyUsed)
                return Result.Fail("Email has already been used");
        }

        Email = email;
        Lastname = lastname;
        Firstname = firstname;
        Password = password;

        return Result.Ok();
    }

    public Result<TargetApp> AddTargetApp(Name name,
        UrlAddress urlAddress,
        int monitoringIntervalInSeconds)
    {
        var targetAppResult = TargetApp.Create(name, urlAddress, monitoringIntervalInSeconds, Id);

        if (targetAppResult.IsFailed)
            return targetAppResult;

        _targetApps.Add(targetAppResult.Value);
        return targetAppResult;
    }

    public Result<TargetApp> UpdateTargetApp(int targetAppId,
        Name name,
        UrlAddress urlAddress,
        int monitoringIntervalInSeconds)
    {
        var targetApp = _targetApps.Where(c=> c.Id == targetAppId).FirstOrDefault();

        if (targetApp is null)
           return Result.Fail("TargetApp not found");

        var result = targetApp.Update(name, urlAddress, monitoringIntervalInSeconds);

        return result;
    }

    public Result<TargetApp> DeleteTargetApp(int targetAppId)
    {
        var targetApp = _targetApps.Where(c => c.Id == targetAppId).FirstOrDefault();

        if (targetApp is null)
            return Result.Fail("TargetApp not found");

        targetApp.Delete();
        return Result.Ok();
    }

    public Result<Notifier> AddNotifierToTargetApp(int targetAppId,
        string notifierAddress,
        NotifierType notifierType)
    {
        var targetApp = _targetApps.Where(c => c.Id == targetAppId).FirstOrDefault();

        if (targetApp is null)
            return Result.Fail("TargetApp not found");

        var result = targetApp.AddNotifier(notifierAddress, notifierType);

        if (result.IsFailed)
            return Result.Fail(result.Errors);

        return Result.Ok(result.Value);
    }

    public Result SetTargetAppIsNotHealthy(int targetAppId)
    {
        var targetApp = _targetApps.Where(c => c.Id == targetAppId).FirstOrDefault();

        if (targetApp is null)
            return Result.Fail("TargetApp not found");

        targetApp.NotHealty();
        return Result.Ok();
    }

    public Result SetTargetAppIsHealthy(int targetAppId)
    {
        var targetApp = _targetApps.Where(c => c.Id == targetAppId).FirstOrDefault();

        if (targetApp is null)
            return Result.Fail("TargetApp not found");

        targetApp.Healthy();
        return Result.Ok();
    }

    private bool IsEmailModified(Email email) => !Email.Value.Equals(email.Value);
}
