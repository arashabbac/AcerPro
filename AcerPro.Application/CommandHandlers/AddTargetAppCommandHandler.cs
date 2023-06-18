using AcerPro.Application.Commands;
using AcerPro.Application.Jobs;
using AcerPro.Domain.Contracts;
using AcerPro.Domain.ValueObjects;
using FluentResults;
using MediatR;

namespace AcerPro.Application.CommandHandlers;

public class AddTargetAppCommandHandler : IRequestHandler<AddTargetAppCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly UrlSchedulerService _urlSchedulerService;
    public AddTargetAppCommandHandler(IUserRepository userRepository, UrlSchedulerService urlSchedulerService)
    {
        _userRepository = userRepository;
        _urlSchedulerService = urlSchedulerService;
    }

    public async Task<Result<int>> Handle(AddTargetAppCommand request, CancellationToken cancellationToken)
    {
        //*************************************************
        var foundedUser = await _userRepository.FindAsync(request.UserId, cancellationToken) ??
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
        var targetAppResult = foundedUser.AddTargetApp(nameResult.Value, urlAddressResult.Value, request.MonitoringIntervalInSeconds);

        if (targetAppResult.IsFailed)
            return Result.Fail<int>(targetAppResult.Errors);

        //*************************************************
        await _userRepository.SaveAsync(cancellationToken);
        await _urlSchedulerService.RestartAsync(cancellationToken);
        return targetAppResult.Value.Id;
        //*************************************************
    }
}
