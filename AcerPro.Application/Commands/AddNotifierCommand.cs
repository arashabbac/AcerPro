using FluentResults;
using MediatR;

namespace AcerPro.Application.Commands;

public record AddNotifierCommand(int TargetAppId,
    string Address,
    byte NotifierType) : IRequest<Result<int>>
{
    public int UserId { get; set; }
}

