using InternetBank.Application.Authentication.Queries.Common;
using InternetBank.Application.Interfaces;
using MediatR;

namespace InternetBank.Application.Authentication.Queries.GetUser;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserQuery, UserDTO>
{
    private readonly IIdentityService _identityService;

    public GetUserByIdQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await _identityService.GetByIdAsync(request.Id);
    }
}
