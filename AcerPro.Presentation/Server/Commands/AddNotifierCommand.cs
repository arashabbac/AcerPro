using FluentResults;
using MediatR;

namespace AcerPro.Presentation.Server.Commands;

public record AddNotifierCommand(int TargetAppId,
    string Address,
    byte NotifierType) : IRequest<Result<int>>
{
    public int UserId { get; set; }
}

