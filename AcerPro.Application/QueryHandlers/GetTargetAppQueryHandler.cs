using AcerPro.Application.Queries;
using AcerPro.Persistence.DTOs;
using AcerPro.Persistence.QueryRepositories;
using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AcerPro.Application.QueryHandlers;

public class GetTargetAppQueryHandler : IRequestHandler<GetTargetAppQuery, Result<TargetAppDto>>
{
    private readonly ITargetAppQueryRepository _targetAppQueryRepository;

    public GetTargetAppQueryHandler(ITargetAppQueryRepository targetAppQueryRepository)
    {
        _targetAppQueryRepository = targetAppQueryRepository;
    }

    public async Task<Result<TargetAppDto>> Handle(GetTargetAppQuery request, CancellationToken cancellationToken)
    {
        //*************************************************
        var targetApp = await _targetAppQueryRepository.GetByIdAsync(request.UserId, request.TargetAppId);

        if (targetApp is null)
            return Result.Fail<TargetAppDto>("Target app not found");
        //*************************************************

        return targetApp;
    }
}
