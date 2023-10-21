using InternetBank.Application.Authentication.Queries.Common;
using MediatR;

namespace InternetBank.Application.Authentication.Queries.GetUser;

public record GetUserQuery(string Id) : IRequest<UserDTO>;