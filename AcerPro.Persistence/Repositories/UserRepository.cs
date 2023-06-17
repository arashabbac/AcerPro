using AcerPro.Domain.Aggregates;
using AcerPro.Domain.Contracts;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AcerPro.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public bool IsUserExist(Specification<User> specification)
    {
        return DbSet.Any(specification.ToExpression().Compile());
    }

    public bool IsEmailAlreadyUsed(Specification<User> specification)
    {
        return DbSet.Any(specification.ToExpression().Compile());
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await DbSet.FirstOrDefaultAsync(c => c.Email == email.ToLower());
    }

    public async Task<User> GetByIdWithTargetAppAsync(int userId)
    {
        return await DbSet
            .Include(c=> c.TargetApps)
            .FirstOrDefaultAsync(c=> c.Id == userId);
    }

    public async Task<User> GetByIdWithTargetAppAndNotifierAsync(int userId)
    {
        return await DbSet
            .Include(c => c.TargetApps)
            .ThenInclude(c=> c.TargetAppNotifiers)
            .FirstOrDefaultAsync(c => c.Id == userId);
    }
}
