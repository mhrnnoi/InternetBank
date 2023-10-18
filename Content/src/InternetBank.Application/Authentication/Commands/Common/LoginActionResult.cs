namespace InternetBank.Application.Authentication.Commands.Common;

public record LoginActionResult(string Massage,
                                string Id,
                                string Username,
                                string Token);