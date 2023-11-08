using MediatR;

namespace InternetBank.Application.Accounts.Commands.BlockAccount;

public record BlockAccountCommand(string Id,
                                  string UserId) :IRequest<bool>;