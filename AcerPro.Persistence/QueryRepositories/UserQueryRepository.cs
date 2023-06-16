using AcerPro.Domain.Aggregates;
using AcerPro.Persistence.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcerPro.Persistence.QueryRepositories;

public class UserQueryRepository : QueryRepository<User>, IUserQueryRepository
{
    public UserQueryRepository(QueryDatabaseContext queryDatabaseContext) : base(queryDatabaseContext)
    {
    }

    public async Task<IList<UserDto>> GetAllAsync()
    {
        return await DbSet.OrderByDescending(c => c.Id).Select(c => new UserDto
        {
            Id = c.Id,
            Firstname = c.Firstname.Value,
            Lastname = c.Lastname.Value,
            Email = c.Email.Value,
        }).ToListAsync();
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var data = await DbSet.FirstOrDefaultAsync(c => c.Id == id);

        if (data is null)
            return null;

        return new UserDto
        {
            Id = data.Id,
            Email = data.Email.Value,
            Lastname = data.Lastname.Value,
            Firstname = data.Firstname.Value,
        };
    }
}
