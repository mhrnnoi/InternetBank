using MediatR;

namespace InternetBank.Application.Transactions.Commands.Transfer_Money;

public record TransferMoneyCommand(string Otp,
                                   double Amount,
                                   string UserId) : IRequest<string>;