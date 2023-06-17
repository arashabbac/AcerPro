using AcerPro.Domain.Aggregates;
using AcerPro.Persistence.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcerPro.Persistence.QueryRepositories;

public class TargetAppQueryRepository : QueryRepository<TargetApp>, ITargetAppQueryRepository
{
    public TargetAppQueryRepository(QueryDatabaseContext queryDatabaseContext) : base(queryDatabaseContext)
    {
    }

    public async Task<TargetAppDto> GetByIdAsync(int userId, int targetAppId)
    {
        return await DbSet.Where(c => c.Id == userId && c.UserId == userId)
            .Select(c => new TargetAppDto
            {
                Id = c.Id,
                MonitoringIntervalInSeconds = c.MonitoringIntervalInSeconds,
                Name = c.Name.Value,
                UrlAddress = c.UrlAddress.Value,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TargetAppDto>> GetAllTargetAppsWithNotifiersAsync(int userId)
    {
        return await DbSet.Where(c => c.UserId == userId)
            .Select(c => new TargetAppDto
            {
                Id = c.Id,
                MonitoringIntervalInSeconds = c.MonitoringIntervalInSeconds,
                Name = c.Name.Value,
                UrlAddress = c.UrlAddress.Value,
                Notifiers = c.Notifiers.Select(q=> new NotifierDto
                {
                    Address = q.Address,
                    Id = q.Id,
                    NotifierType = (NotifierTypeDto)q.NotifierType,
                }).ToList(),
            })
            .ToListAsync();
    }
}
