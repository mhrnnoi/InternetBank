using InternetBank.Application.Authentication.Queries.Common;
using MediatR;

namespace InternetBank.Application.Authentication.Queries.GetUserById;

public record GetUserByIdQuery(string Id) : IRequest<UserDTO?>;