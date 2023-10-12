using InternetBank.Application.Features.Authentication.Commands.Common;
using MediatR;

namespace InternetBank.Application.Features.Authentication.Commands.Login;

public record LoginCommand(string FirstName,
                              string LastName,
                              string NationalCode,
                              DateTime BirthDate,
                              string PhoneNumber,
                              string Email,
                              string Username,
                              string Password) : IRequest<LoginActionResult>;