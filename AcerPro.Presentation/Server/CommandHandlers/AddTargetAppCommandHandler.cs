using AcerPro.Domain.Contracts;
using AcerPro.Domain.ValueObjects;
using AcerPro.Presentation.Server.Commands;
using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Server.CommandHandlers;

public class AddTargetAppCommandHandler : IRequestHandler<AddTargetAppCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;

    public AddTargetAppCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<int>> Handle(AddTargetAppCommand request, CancellationToken cancellationToken)
    {
        //*************************************************
        var foundedUser = await _userRepository.FindAsync(request.UserId, cancellationToken) ??
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
        var targetAppResult = foundedUser.AddTargetApp(nameResult.Value, urlAddressResult.Value, request.MonitoringIntervalInSeconds);

        if(targetAppResult.IsFailed)
            return Result.Fail<int>(targetAppResult.Errors);

        await _userRepository.SaveAsync(cancellationToken);
        return targetAppResult.Value.Id;
        //*************************************************
    }
}
