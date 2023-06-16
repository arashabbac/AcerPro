using AcerPro.Domain.Contracts;
using AcerPro.Persistence.DTOs;
using AcerPro.Presentation.Server.Commands;
using AcerPro.Presentation.Server.Infrastructures;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Server.CommandHandlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(IUserRepository userRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<Result<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var foundedUser = await _userRepository.GetByEmailAsync(request.Email);

        if (foundedUser is null)
            return Result.Fail<UserDto>("Username or password is not correct");

        if(foundedUser.Password.Verify(request.Password) == false)
            return Result.Fail<UserDto>("Username or password is not correct");

        var tokenResult = JwtUtility.GenerateJwtToken(foundedUser.Email.Value, foundedUser.Id, _configuration["SecretKey"]);

        return new UserDto
        {
            Email = foundedUser.Email.Value,
            Firstname = foundedUser.Firstname.Value,
            Id = foundedUser.Id,
            Lastname = foundedUser.Lastname.Value,
            Token = tokenResult.Item1,
            ExpireIn = tokenResult.Item2
        };
    }
}
