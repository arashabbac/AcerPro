using AcerPro.Common;
using FluentResults;
using Framework.Domain;
using Framework.Extensions;
using System.Text.RegularExpressions;

namespace AcerPro.Domain.ValueObjects;

public sealed class UrlAddress : ValueObject
{
    #region Static Member(s)
    public const int MaxLenght = 1000;
    public static Regex ValidUrlRegex = new(Constants.Regex.Url);


    public static Result<UrlAddress> Create(string value)
    {
        value = value.Fix();

        if (value.IsNullOrWhiteSpace())
            return Result.Fail<UrlAddress>("UrlAddress value must not be empty");

        if (value.Length > MaxLenght)
            return Result.Fail<UrlAddress>($"UrlAddress value must be less than {MaxLenght} characters");

        if (ValidUrlRegex.IsMatch(value) == false)
            return Result.Fail<UrlAddress>("UrlAddress value is not valid");

        return Result.Ok(new UrlAddress(value.ToLower()));
    }
    #endregion

    //For EF Core!
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private UrlAddress()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    private UrlAddress(string value)
    {
        Value = value;
    }


    public string Value { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
