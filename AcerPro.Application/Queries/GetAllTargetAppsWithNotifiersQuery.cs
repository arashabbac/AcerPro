using AcerPro.Persistence.DTOs;
using FluentResults;
using MediatR;
using System.Collections.Generic;

namespace AcerPro.Application.Queries;

public record GetAllTargetAppsWithNotifiersQuery(int UserId) : IRequest<Result<IEnumerable<TargetAppDto>>>;
