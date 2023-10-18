using InternetBank.Application.Authentication.Commands.Common;
using MediatR;

namespace InternetBank.Application.Authentication.Commands.Login;

public record LoginCommand(string Email,
                           string Password) : IRequest<LoginActionResult?>;