using ErrorOr;
using MediatR;

namespace InternetBank.Application.Accounts.Commands.UnBlockAccount;

public record UnBlockAccountCommand(string AccountId,
                                    string UserId) : IRequest<ErrorOr<bool>>;