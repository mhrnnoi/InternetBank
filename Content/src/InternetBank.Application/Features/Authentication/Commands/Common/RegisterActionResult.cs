namespace InternetBank.Application.Features.Authentication.Commands.Common;

public record RegisterActionResult(string Massage,
                                  string Id,
                                  string Location,
                                  string Token);