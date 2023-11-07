using InternetBank.Application.Authentication.Queries.Common;
using InternetBank.Application.Interfaces;
using MediatR;

namespace InternetBank.Application.Authentication.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDTO>>
{
    private readonly IIdentityService _identityService;

    public GetUsersQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<List<UserDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _identityService.GetAllAsync();
    }
}
