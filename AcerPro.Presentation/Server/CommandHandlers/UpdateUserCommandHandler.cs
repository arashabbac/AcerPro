using AcerPro.Domain.Contracts;
using AcerPro.Domain.ValueObjects;
using AcerPro.Presentation.Server.Commands;
using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Server.CommandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        //*************************************************
        var foundedUser = await _userRepository.FindAsync(request.Id, cancellationToken);

        if (foundedUser is null)
            throw new System.ApplicationException("The user not found! it might be deleted. contact your system provider!");
        //*************************************************

        var firstnameResult = Name.Create(request.Firstname);
        var lastnameResult = Name.Create(request.Lastname);
        var emailResult = Email.Create(request.Email);
        var passwordResult = Password.Create(request.Password);

        var result = Result.Merge(firstnameResult, lastnameResult, emailResult,passwordResult);

        if (result.IsFailed)
            return Result.Fail<int>(result.Errors);

        //*************************************************
        var updateUserResult = foundedUser.Update(firstnameResult.Value,
            lastnameResult.Value,
            emailResult.Value,
            passwordResult.Value,
            _userRepository);

        if (updateUserResult.IsFailed)
            return Result.Fail<int>(updateUserResult.Errors);
        //*************************************************

        await _userRepository.SaveAsync(cancellationToken);

        return foundedUser.Id;
    }
}
