using AcerPro.Application.Commands;
using AcerPro.Application.Jobs;
using AcerPro.Domain.Contracts;
using FluentResults;
using MediatR;

namespace AcerPro.Application.CommandHandlers;

public class AddNotifierCommandHandler : IRequestHandler<AddNotifierCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly UrlSchedulerService _urlSchedulerService;
    public AddNotifierCommandHandler(IUserRepository userRepository, UrlSchedulerService urlSchedulerService)
    {
        _userRepository = userRepository;
        _urlSchedulerService = urlSchedulerService;
    }

    public async Task<Result<int>> Handle(AddNotifierCommand request, CancellationToken cancellationToken)
    {
        //*************************************************
        var foundedUser = await _userRepository.GetByIdWithTargetAppAndNotifierAsync(request.UserId) ??
            throw new ApplicationException("The user not found! it might be deleted. contact your system provider!");
        //*************************************************

        //*************************************************
        var notifierResult = foundedUser.AddNotifierToTargetApp(request.TargetAppId, request.Address, (Domain.Enums.NotifierType)request.NotifierType!);

        if (notifierResult.IsFailed)
            return Result.Fail<int>(notifierResult.Errors);

        //*************************************************
        await _userRepository.SaveAsync(cancellationToken);
        await _urlSchedulerService.RestartAsync(cancellationToken);
        return notifierResult.Value.Id;
        //*************************************************
    }
}
