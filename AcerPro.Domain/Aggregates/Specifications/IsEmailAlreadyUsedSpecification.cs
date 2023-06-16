using AcerPro.Domain.Aggregates;
using Framework.Domain;
using System.Linq.Expressions;

namespace AcerPro.Domain.Aggregates.Specifications;

public class IsEmailAlreadyUsedSpecification : Specification<User>
{
    private readonly string _email;
    public IsEmailAlreadyUsedSpecification(string email)
    {
        _email = email;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return customer => customer.Email.Value.Equals(_email, StringComparison.OrdinalIgnoreCase);
    }
}
