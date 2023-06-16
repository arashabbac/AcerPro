using AcerPro.Domain.Aggregates;
using AcerPro.Domain.Contracts;
using AcerPro.Domain.ValueObjects;
using AcerPro.Presentation.Server.Commands;
using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Server.CommandHandlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        var lastnameResult = Name.Create(request.Lastname);
        var firstnameResult = Name.Create(request.Firstname);
        var passwordResult = Password.Create(request.Password); 

        var result = Result.Merge(firstnameResult, lastnameResult, emailResult,passwordResult);

        if (result.IsFailed)
            return Result.Fail<int>(result.Errors);

        //*************************************************
        var userResult = User.Create(firstnameResult.Value,
            lastnameResult.Value,
            emailResult.Value,
            passwordResult.Value,
            _userRepository);

        if (userResult.IsFailed)
            return Result.Fail<int>(userResult.Errors);
        //*************************************************

        await _userRepository.AddAsync(userResult.Value, cancellationToken);
        await _userRepository.SaveAsync(cancellationToken);

        return userResult.Value.Id;
    }
}