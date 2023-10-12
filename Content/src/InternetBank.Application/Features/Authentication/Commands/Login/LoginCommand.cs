using InternetBank.Application.Features.Authentication.Commands.Common;
using MediatR;

namespace InternetBank.Application.Features.Authentication.Commands.Login;

public record LoginCommand(string Email,
                              string Password) : IRequest<LoginActionResult>;