using FluentResults;
using MediatR;

namespace AcerPro.Application.Commands;

public record UpdateTargetAppCommand(string Name,
    string UrlAddress,
    int MonitoringIntervalInSeconds) : IRequest<Result<int>>
{
    public int UserId { get; set; }
    public int TargetAppId { get; set; }
}