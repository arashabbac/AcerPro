using FluentResults;
using MediatR;

namespace AcerPro.Application.Commands;

public record DeleteTargetAppCommand(int TargetAppId,int UserId) : IRequest<Result>;

