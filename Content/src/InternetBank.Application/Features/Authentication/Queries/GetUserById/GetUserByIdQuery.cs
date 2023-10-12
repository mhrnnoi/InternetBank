using InternetBank.Application.Features.Authentication.Queries.Common;
using MediatR;

namespace InternetBank.Application.Features.Authentication.Queries.GetUserById;

public record GetUserByIdQuery(string Id) : IRequest<UserDTO?>;