using FluentResults;
using MediatR;

namespace AcerPro.Presentation.Server.Commands;

public record RegisterUserCommand(string Firstname,
    string Lastname,
    string Email,
    string Password,
    string ConfirmedPassword) : IRequest<Result<int>>;
