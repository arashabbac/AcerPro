using AcerPro.Domain.Enums;
using AcerPro.Domain.ValueObjects;
using FluentResults;
using Framework.Domain;
using System.Text.RegularExpressions;
using Framework.Extensions;


namespace AcerPro.Domain.Aggregates;

public class Notifier : Entity<int>
{
    #region Constant(s)
    public const int AddressMaxLength = 200;
    public static Regex ValidPhoneNumberRegex = new(@"^\d{11}$");

    public static Result<Notifier> Create(string address,NotifierType notifierType)
    {
        address = address.Fix();

        if (address.IsNullOrWhiteSpace())
            return Result.Fail("Address must not be empty");

        if (address.Length > AddressMaxLength)
            return Result.Fail<Notifier>($"Name value must be less than {AddressMaxLength} characters");

        var checkNotifierAddressByItsType = CheckNotifierAddressByItsType(address, notifierType);

        if (checkNotifierAddressByItsType.IsFailed)
            return Result.Fail<Notifier>(checkNotifierAddressByItsType.Errors);

        return Result.Ok(new Notifier(address, notifierType));
    }
    #endregion
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Notifier() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Notifier(string address,
        NotifierType notifierType)
    {
        Address = address;
        NotifierType = notifierType;
    }

    public string Address { get; private set; }
    public NotifierType NotifierType { get; private set; }
    public bool IsDeleted { get; private set; }

    private readonly List<TargetAppNotifier> _targetAppNotifiers = new();
    public IReadOnlyList<TargetAppNotifier> TargetAppNotifiers => _targetAppNotifiers;

    internal Result Update(string address)
    {
        address = address.Fix();

        if (address.IsNullOrWhiteSpace())
            return Result.Fail("Address must not be empty");

        if (address.Length > AddressMaxLength)
            return Result.Fail($"Name value must be less than {AddressMaxLength} characters");

        Address = address;
        return Result.Ok();
    }

    internal void Delete() => IsDeleted = true;

    private static Result CheckNotifierAddressByItsType(string address, NotifierType notifierType)
    {
        if (notifierType == NotifierType.Email && Email.ValidEmailRegex.IsMatch(address) == false)
            return Result.Fail("Email address is not correct");

        if ((notifierType == NotifierType.SMS || notifierType == NotifierType.Call) && ValidPhoneNumberRegex.IsMatch(address) == false)
            return Result.Fail("Phone number is not correct");

        return Result.Ok();
    }
}
