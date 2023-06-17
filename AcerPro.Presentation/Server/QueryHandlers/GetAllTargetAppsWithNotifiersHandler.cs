using AcerPro.Persistence.DTOs;
using AcerPro.Persistence.QueryRepositories;
using AcerPro.Presentation.Server.Queries;
using FluentResults;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Server.QueryHandlers;

public class GetAllTargetAppsWithNotifiersHandler : IRequestHandler<GetAllTargetAppsWithNotifiersQuery, Result<IEnumerable<TargetAppDto>>>
{
    private readonly ITargetAppQueryRepository _targetAppQueryRepository;

    public GetAllTargetAppsWithNotifiersHandler(ITargetAppQueryRepository targetAppQueryRepository)
    {
        _targetAppQueryRepository = targetAppQueryRepository;
    }

    public async Task<Result<IEnumerable<TargetAppDto>>> Handle(GetAllTargetAppsWithNotifiersQuery request, CancellationToken cancellationToken)
    {
        var targetApps = await _targetAppQueryRepository.GetAllTargetAppsWithNotifiersAsync(request.UserId);

        if (targetApps is null)
            return Result.Ok(Enumerable.Empty<TargetAppDto>());

        return Result.Ok(targetApps);
    }
}
