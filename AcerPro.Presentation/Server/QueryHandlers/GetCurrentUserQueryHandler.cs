using AcerPro.Persistence.DTOs;
using AcerPro.Persistence.QueryRepositories;
using AcerPro.Presentation.Server.Queries;
using FluentResults;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Server.QueryHandlers;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, Result<UserDto>>
{
    private readonly IUserQueryRepository _userQueryRepository;

    public GetCurrentUserQueryHandler(IUserQueryRepository userQueryRepository)
    {
        _userQueryRepository = userQueryRepository;
    }

    public async Task<Result<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        //*************************************************
        var foundedUser = await _userQueryRepository.FindAsync(request.UserId, cancellationToken) ??
            throw new System.ApplicationException("The user not found! it might be deleted. contact your system provider!");
        //*************************************************

        return new UserDto
        {
            Id = foundedUser.Id,
            Email = foundedUser.Email.Value,
            Firstname = foundedUser.Firstname.Value,
            Lastname = foundedUser.Lastname.Value,
        };
    }
}
