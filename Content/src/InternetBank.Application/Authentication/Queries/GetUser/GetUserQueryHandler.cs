using InternetBank.Application.Authentication.Queries.Common;
using InternetBank.Application.Interfaces;
using InternetBank.Domain.Exceptions;
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
        var userDTO = await _identityService.GetByIdAsync(request.Id);

        return userDTO;
    }
}
