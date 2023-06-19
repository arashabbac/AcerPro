using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AcerPro.Presentation.Server.Infrastructures;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    public BaseController(ISender sender,ILogger<ControllerBase> logger)
    {
        Sender = sender;
        Logger = logger;
    }

    protected ISender Sender { get; }
    protected ILogger<ControllerBase> Logger { get; }

    protected IActionResult APIResult<T>(Result<T> result)
    {
        if (result.IsFailed)
        {
            Logger.LogError(result.GetResultMessage());
            return BadRequest(result.ToAPIResponse());
        }

        return Ok(result.ToAPIResponse());
    }

    protected IActionResult APIResult(Result result)
    {
        if (result.IsFailed)
        {
            Logger.LogError(result.GetResultMessage());
            return BadRequest(result.ToAPIResponse());
        }

        return Ok(result.ToAPIResponse());
    }
}
