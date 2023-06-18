using FluentResults;
using MediatR;
using System.Text.Json.Serialization;

namespace AcerPro.Application.Commands;

public record UpdateUserCommand(string Firstname,
    string Lastname,
    string Email,
    string Password,
    string ConfirmedPassword) : IRequest<Result<int>>
{
    [JsonIgnore]
    public int Id { get; set; }
}
