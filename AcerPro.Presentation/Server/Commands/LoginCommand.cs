using AcerPro.Persistence.DTOs;
using FluentResults;
using MediatR;

namespace AcerPro.Presentation.Server.Commands;

public record LoginCommand(string Email,
    string Password): IRequest<Result<UserDto>>;