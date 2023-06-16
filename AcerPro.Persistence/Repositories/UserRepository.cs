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
}
