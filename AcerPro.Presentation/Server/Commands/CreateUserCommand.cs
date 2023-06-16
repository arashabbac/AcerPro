using FluentResults;
using MediatR;

namespace AcerPro.Presentation.Server.Commands;

public record CreateUserCommand(string Firstname,
    string Lastname,
    string Email,
    string Password,
    string ConfirmedPassword) : IRequest<Result<int>>;
