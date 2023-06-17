using AcerPro.Domain.Contracts;
using AcerPro.Presentation.Server.Commands;
using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Server.CommandHandlers;

public class AddNotifierCommandHandler : IRequestHandler<AddNotifierCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;

    public AddNotifierCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<int>> Handle(AddNotifierCommand request, CancellationToken cancellationToken)
    {
        //*************************************************
        var foundedUser = await _userRepository.GetByIdWithTargetAppAndNotifierAsync(request.UserId) ??
            throw new System.ApplicationException("The user not found! it might be deleted. contact your system provider!");
        //*************************************************

        //*************************************************
        var notifierResult = foundedUser.AddNotifierToTargetApp(request.TargetAppId,request.Address,(Domain.Enums.NotifierType)request.NotifierType!);

        if (notifierResult.IsFailed)
            return Result.Fail<int>(notifierResult.Errors);

        await _userRepository.SaveAsync(cancellationToken);
        return notifierResult.Value.Id;
        //*************************************************
    }
}
