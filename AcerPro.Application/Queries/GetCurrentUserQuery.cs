using AcerPro.Persistence.DTOs;
using FluentResults;
using MediatR;

namespace AcerPro.Application.Queries;

public record GetCurrentUserQuery(int UserId) : IRequest<Result<UserDto>>;
