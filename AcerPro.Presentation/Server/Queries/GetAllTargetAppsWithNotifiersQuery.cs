using AcerPro.Persistence.DTOs;
using FluentResults;
using MediatR;
using System.Collections.Generic;

namespace AcerPro.Presentation.Server.Queries;

public record GetAllTargetAppsWithNotifiersQuery(int UserId) : IRequest<Result<IEnumerable<TargetAppDto>>>;
