using AcerPro.Domain.Contracts;
using AcerPro.Domain.ValueObjects;
using AcerPro.Presentation.Server.Commands;
using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Server.CommandHandlers;

public class UpdateTargetAppCommandHandler : IRequestHandler<UpdateTargetAppCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;

    public UpdateTargetAppCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<int>> Handle(UpdateTargetAppCommand request, CancellationToken cancellationToken)
    {
        //*************************************************
        var foundedUser = await _userRepository.GetByIdWithTargetAppAsync(request.UserId) ??
            throw new System.ApplicationException("The user not found! it might be deleted. contact your system provider!");
        //*************************************************

        //*************************************************
        var nameResult = Name.Create(request.Name);
        var urlAddressResult = UrlAddress.Create(request.UrlAddress);

        var result = Result.Merge(nameResult, urlAddressResult);

        if (result.IsFailed)
            return Result.Fail<int>(result.Errors);
        //*************************************************

        //*************************************************
        var targetAppResult = foundedUser.UpdateTargetApp(request.TargetAppId,nameResult.Value, urlAddressResult.Value, request.MonitoringIntervalInSeconds);

        if (targetAppResult.IsFailed)
            return Result.Fail<int>(targetAppResult.Errors);

        await _userRepository.SaveAsync(cancellationToken);
        return targetAppResult.Value.Id;
        //*************************************************
    }
}
