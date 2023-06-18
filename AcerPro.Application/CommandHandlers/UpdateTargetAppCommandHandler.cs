using AcerPro.Application.Commands;
using AcerPro.Application.Jobs;
using AcerPro.Domain.Contracts;
using AcerPro.Domain.ValueObjects;
using FluentResults;
using MediatR;

namespace AcerPro.Application.CommandHandlers;

public class UpdateTargetAppCommandHandler : IRequestHandler<UpdateTargetAppCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly UrlSchedulerService _urlSchedulerService;
    public UpdateTargetAppCommandHandler(IUserRepository userRepository,
        UrlSchedulerService urlSchedulerService)
    {
        _userRepository = userRepository;
        _urlSchedulerService = urlSchedulerService;
    }

    public async Task<Result<int>> Handle(UpdateTargetAppCommand request, CancellationToken cancellationToken)
    {
        //*************************************************
        var foundedUser = await _userRepository.GetByIdWithTargetAppAsync(request.UserId) ??
            throw new ApplicationException("The user not found! it might be deleted. contact your system provider!");
        //*************************************************

        //*************************************************
        var nameResult = Name.Create(request.Name);
        var urlAddressResult = UrlAddress.Create(request.UrlAddress);

        var result = Result.Merge(nameResult, urlAddressResult);

        if (result.IsFailed)
            return Result.Fail<int>(result.Errors);
        //*************************************************

        //*************************************************
        var targetAppResult = foundedUser.UpdateTargetApp(request.TargetAppId, nameResult.Value, urlAddressResult.Value, request.MonitoringIntervalInSeconds);

        if (targetAppResult.IsFailed)
            return Result.Fail<int>(targetAppResult.Errors);

        //*************************************************
        await _userRepository.SaveAsync(cancellationToken);
        await _urlSchedulerService.RestartAsync(cancellationToken);
        return targetAppResult.Value.Id;
        //*************************************************
    }
}
