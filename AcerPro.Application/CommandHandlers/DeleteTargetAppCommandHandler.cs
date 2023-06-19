using AcerPro.Application.Commands;
using AcerPro.Application.Jobs;
using AcerPro.Domain.Contracts;
using FluentResults;
using MediatR;

namespace AcerPro.Application.CommandHandlers;

public class DeleteTargetAppCommandHandler : IRequestHandler<DeleteTargetAppCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly UrlSchedulerService _urlSchedulerService;
    public DeleteTargetAppCommandHandler(IUserRepository userRepository,
        UrlSchedulerService urlSchedulerService)
    {
        _userRepository = userRepository;
        _urlSchedulerService = urlSchedulerService;
    }

    public async Task<Result> Handle(DeleteTargetAppCommand request, CancellationToken cancellationToken)
    {
        //*************************************************
        var foundedUser = await _userRepository.GetByIdWithTargetAppAsync(request.UserId) ??
            throw new ApplicationException("The user not found! it might be deleted. contact your system provider!");
        //*************************************************

        //*************************************************
        var targetAppResult = foundedUser.DeleteTargetApp(request.TargetAppId);

        if (targetAppResult.IsFailed)
            return Result.Fail(targetAppResult.Errors);

        //*************************************************
        await _userRepository.SaveAsync(cancellationToken);
        await _urlSchedulerService.RestartAsync(cancellationToken);
        return Result.Ok().WithSuccess("Target App has been deleted");
        //*************************************************
    }
}
