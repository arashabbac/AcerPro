using AcerPro.Common;
using FluentResults;
using System.Linq;

namespace AcerPro.Presentation.Server.Infrastructures;

public static class FluentResultExtensions
{
    public static object ToAPIResponse(this Result result) => new APIResponseModel
    {
        IsSuccess = result.IsSuccess,
        Messages = result.GetResultMessages(),
    };

    public static object ToAPIResponse<T>(this Result<T> result) => new APIResponseModel<T>
    {
        IsSuccess = result.IsSuccess,
        Messages = result.GetResultMessages(),
        Data = result.ValueOrDefault
    };

    public static string[] GetResultMessages<T>(this Result<T> result)
    {
        return result.Reasons.Select(c => c.Message).Concat(result.IsSuccess ? result.Successes.Select(c => c.Message) : result.Errors.Select(c => c.Message)).Distinct().ToArray();
    }

    public static string[] GetResultMessages(this Result result)
    {
        return result.Reasons.Select(c => c.Message).Concat(result.IsSuccess ? result.Successes.Select(c => c.Message) : result.Errors.Select(c => c.Message)).Distinct().ToArray();
    }

    public static string GetResultMessage(this Result result)
    {
        return string.Join(",",result.Reasons.Select(c => c.Message).Concat(result.IsSuccess ? result.Successes.Select(c => c.Message) : result.Errors.Select(c => c.Message)).Distinct());
    }

    public static string GetResultMessage<T>(this Result<T> result)
    {
        return string.Join(",", result.Reasons.Select(c => c.Message).Concat(result.IsSuccess ? result.Successes.Select(c => c.Message) : result.Errors.Select(c => c.Message)).Distinct());
    }
}
