using MediatR;

namespace InternetBank.Application.Accounts.Commands.UnBlockAccount;

public record UnBlockAccountCommand(string Id,
                                    string UserId) : IRequest<bool>;