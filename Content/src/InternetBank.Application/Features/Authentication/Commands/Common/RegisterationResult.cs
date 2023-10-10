namespace InternetBank.Application.Features.Authentication.Commands.Common;

public record RegisterationResult(string Massage,
                                  string Id,
                                  string Location,
                                  string Token);