using AcerPro.Common;
using FluentResults;
using Framework.Domain;
using Framework.Extensions;
using System.Text.RegularExpressions;

namespace AcerPro.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    #region Static Member(s)
    public const int MaxLenght = 150;
    public const int MinLenght = 10;
    public static Regex ValidEmailRegex = new(Constants.Regex.Email);

    public static Result<Email> Create(string value)
    {
        value = value.Fix();

        if (value.IsNullOrWhiteSpace())
            return Result.Fail<Email>("Email value must not be empty");

        if (value.Length < MinLenght)
            return Result.Fail<Email>($"Email value must be greater than {MinLenght} characters");

        if (value.Length > MaxLenght)
            return Result.Fail<Email>($"Email value must be less than {MaxLenght} characters");

        if (ValidEmailRegex.IsMatch(value) == false)
            return Result.Fail<Email>("Email value is not valid");

        return Result.Ok(new Email(value.ToLower()));
    }
    #endregion

    //For EF Core!
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Email()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(Email value)
    {
        return value.Value;
    }

    public static explicit operator Email(string value)
    {
        return new Email(value);
    }
}
