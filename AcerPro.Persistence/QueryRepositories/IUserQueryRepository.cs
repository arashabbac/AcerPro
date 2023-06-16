using AcerPro.Domain.Aggregates;
using AcerPro.Persistence.DTOs;
using Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcerPro.Persistence.QueryRepositories;

public interface IUserQueryRepository : IQueryRepository<User>
{
    Task<IList<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(int id);
}