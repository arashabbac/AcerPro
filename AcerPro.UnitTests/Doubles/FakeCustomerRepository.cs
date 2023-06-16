using AcerPro.Domain.Aggregates;
using AcerPro.Domain.Contracts;
using Framework.Domain;
using System.Linq.Expressions;

namespace AcerPro.UnitTests.Doubles;

public class FakeUserRepository : IUserRepository
{

    private readonly List<User> _users;

    public FakeUserRepository()
    {
        _users = new List<User>();
    }

    public Task<User> AddAsync(User entity, CancellationToken cancellationToken = default)
    {
        _users.Add(entity);
        return Task.FromResult(entity);
    }

    public Task AddRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public bool IsUserExist(Specification<User> specification)
    {
        return _users.Any(specification.ToExpression().Compile());
    }

    public bool IsEmailAlreadyUsed(Specification<User> specification)
    {
        return _users.Any(specification.ToExpression().Compile());
    }

    public Task<IEnumerable<User>> SelectAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}
