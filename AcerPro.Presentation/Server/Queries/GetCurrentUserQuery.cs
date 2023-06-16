using AcerPro.Persistence.DTOs;
using FluentResults;
using MediatR;

namespace AcerPro.Presentation.Server.Queries;

public record GetCurrentUserQuery(int UserId) : IRequest<Result<UserDto>>;
