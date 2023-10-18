using InternetBank.Application.Authentication.Queries.Common;
using InternetBank.Application.Interfaces;
using InternetBank.Domain.Exceptions;
using MediatR;

namespace InternetBank.Application.Authentication.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO?>
{
    private readonly IIdentityService _identityService;

    public GetUserByIdQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<UserDTO?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userDTO = await _identityService.GetByIdAsync(request.Id);

        return userDTO;
    }
}
