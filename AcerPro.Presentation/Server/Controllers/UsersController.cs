using AcerPro.Persistence.DTOs;
using AcerPro.Presentation.Server.Commands;
using AcerPro.Presentation.Server.Infrastructures;
using AcerPro.Presentation.Server.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Server.Controllers;

public class UsersController : BaseController
{
    public UsersController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender)
    {
        AuthenticatedUser = httpContextAccessor.HttpContext?.Items["User"] as UserDto;
    }

    public UserDto AuthenticatedUser { get; }
        
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
    {
        var result = await Sender.Send(command);

        return APIResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await Sender.Send(command);

        return APIResult(result);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
    {
        command.Id = AuthenticatedUser.Id;
        var result = await Sender.Send(command);

        return APIResult(result);
    }

    [HttpGet("get-current-user")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var result = await Sender.Send(new GetCurrentUserQuery(AuthenticatedUser.Id));

        return APIResult(result);
    }

    [HttpPost("target-app")]
    [Authorize]
    public async Task<IActionResult> TargetApp([FromBody] AddTargetAppCommand command)
    {
        command.UserId = AuthenticatedUser.Id;
        var result = await Sender.Send(command);

        return APIResult(result);
    }

    [HttpPut("target-app/{id}")]
    [Authorize]
    public async Task<IActionResult> TargetApp(int id,[FromBody] UpdateTargetAppCommand command)
    {
        command.TargetAppId = id;
        command.UserId = AuthenticatedUser.Id;
        var result = await Sender.Send(command);

        return APIResult(result);
    }

    [HttpGet("target-app/{id}")]
    [Authorize]
    public async Task<IActionResult> TargetApp(int id)
    {
        var result = await Sender.Send(new GetTargetAppQuery(UserId:AuthenticatedUser.Id,TargetAppId:id));

        return APIResult(result);
    }

    [HttpGet("/target-app")]
    [Authorize]
    public async Task<IActionResult> TargetApp()
    {
        var result = await Sender.Send(new GetAllTargetAppQuery(UserId: AuthenticatedUser.Id));

        return APIResult(result);
    }
}
