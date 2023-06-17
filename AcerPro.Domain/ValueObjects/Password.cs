using AcerPro.Common;
using FluentResults;
using Framework.Domain;
using Framework.Extensions;
using System.Text.RegularExpressions;

namespace AcerPro.Domain.ValueObjects;

public sealed class Password : ValueObject
{
    #region Static Member(s)
    public const int MaxLenght = 64;
    public static Regex PasswordRegex = new(Constants.Regex.Password);
    public static Result<Password> Create(string value)
    {
        value = value.Fix();

        if (value.IsNullOrWhiteSpace())
            return Result.Fail<Password>("Password value must not be empty");

        if (PasswordRegex.IsMatch(value) == false)
            return Result.Fail<Password>($"Password lenght must be between 8 to 40 and must contain upper case, lower case and symbol characters");

        return Result.Ok(new Password(Framework.Utility.Hashing.GetSha1(value)));
    }
    #endregion

    //For EF Core!
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Password()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    private Password(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public bool Verify(string input)
    {
        return string.Compare(Value,
                Framework.Utility.Hashing.GetSha1(input),
                ignoreCase: false) == 0;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static explicit operator Password(string value)
    {
        return new Password(value);
    }

}
