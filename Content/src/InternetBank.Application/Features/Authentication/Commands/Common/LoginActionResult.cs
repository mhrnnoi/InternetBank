namespace InternetBank.Application.Features.Authentication.Commands.Common;

public record LoginActionResult(string Massage,
                                string Id,
                                string Token,
                                string Username);