using FluentResults;
using MediatR;

namespace AcerPro.Application.Commands;

public record RegisterUserCommand(string Firstname,
    string Lastname,
    string Email,
    string Password,
    string ConfirmedPassword) : IRequest<Result<int>>;
