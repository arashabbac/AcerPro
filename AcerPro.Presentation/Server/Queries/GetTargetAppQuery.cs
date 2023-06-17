using AcerPro.Persistence.DTOs;
using FluentResults;
using MediatR;
using System.Collections.Generic;

namespace AcerPro.Presentation.Server.Queries;

public record GetTargetAppQuery(int UserId,
    int TargetAppId) : IRequest<Result<TargetAppDto>>;

public record GetAllTargetAppQuery(int UserId) : IRequest<Result<IReadOnlyList<TargetAppDto>>>;
