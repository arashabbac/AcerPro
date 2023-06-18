using AcerPro.Persistence.DTOs;
using FluentResults;
using MediatR;

namespace AcerPro.Application.Queries;

public record GetTargetAppQuery(int UserId,
    int TargetAppId) : IRequest<Result<TargetAppDto>>;
