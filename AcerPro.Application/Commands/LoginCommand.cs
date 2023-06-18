using AcerPro.Persistence.DTOs;
using FluentResults;
using MediatR;

namespace AcerPro.Application.Commands;

public record LoginCommand(string Email,
    string Password) : IRequest<Result<UserDto>>;