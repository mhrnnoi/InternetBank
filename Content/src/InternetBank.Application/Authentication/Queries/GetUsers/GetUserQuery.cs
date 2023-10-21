using InternetBank.Application.Authentication.Queries.Common;
using MediatR;

namespace InternetBank.Application.Authentication.Queries.GetUsers;

public record GetUsersQuery() : IRequest<List<UserDTO>>;