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
        return await DbSet.Where(c => c.UserId == userId && c.Id == targetAppId)
            .Select(c => new TargetAppDto
            {
                Id = c.Id,
                MonitoringIntervalInSeconds = c.MonitoringIntervalInSeconds,
                Name = c.Name,
                UrlAddress = c.UrlAddress,
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
                Name = c.Name,
                UrlAddress = c.UrlAddress,
                LastDownDateTime = c.LastDownDateTime,
                IsHealthy = c.IsHealthy,
                LastModifiedDateTime = c.ModifiedDateTime,
                Notifiers = c.Notifiers.Select(q=> new NotifierDto
                {
                    Address = q.Address,
                    Id = q.Id,
                    NotifierType = (NotifierTypeDto)q.NotifierType,
                }).ToList(),
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<TargetAppDto>> GetAllTargetAppsWithNotifiersAsync()
    {
        return await DbSet
            .Select(c => new TargetAppDto
            {
                Id = c.Id,
                MonitoringIntervalInSeconds = c.MonitoringIntervalInSeconds,
                Name = c.Name,
                UrlAddress = c.UrlAddress,
                UserId = c.UserId,
                Notifiers = c.Notifiers.Select(q => new NotifierDto
                {
                    Address = q.Address,
                    Id = q.Id,
                    NotifierType = (NotifierTypeDto)q.NotifierType,
                }).ToList(),
            })
            .ToListAsync();
    }
}
