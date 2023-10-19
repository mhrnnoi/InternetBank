namespace InternetBank.Application.Authentication.Commands.Login;

public record LoginActionResult(string Massage,
                                string Id,
                                string Token);