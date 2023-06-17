using AcerPro.Domain.Aggregates;
using Framework.Domain;

namespace AcerPro.Domain.Contracts;

public interface IUserRepository : IRepository<User>
{
    bool IsEmailAlreadyUsed(Specification<User> specification);
    bool IsUserExist(Specification<User> specification);
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByIdWithTargetAppAsync(int userId);
    Task<User> GetByIdWithTargetAppAndNotifierAsync(int userId);
}