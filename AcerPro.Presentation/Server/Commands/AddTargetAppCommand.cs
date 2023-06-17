using FluentResults;
using MediatR;

namespace AcerPro.Presentation.Server.Commands;

public record AddTargetAppCommand(string Name,
    string UrlAddress,
    int MonitoringIntervalInSeconds) : IRequest<Result<int>>
{
    public int UserId { get; set; }
}
