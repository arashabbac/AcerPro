using AcerPro.Domain.Aggregates;
using AcerPro.Persistence.DTOs;
using Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcerPro.Persistence.QueryRepositories;

public interface ITargetAppQueryRepository : IQueryRepository<TargetApp>
{
    Task<TargetAppDto> GetByIdAsync(int userId, int targetAppId);
    Task<IEnumerable<TargetAppDto>> GetAllTargetAppsWithNotifiersAsync(int userId);
}