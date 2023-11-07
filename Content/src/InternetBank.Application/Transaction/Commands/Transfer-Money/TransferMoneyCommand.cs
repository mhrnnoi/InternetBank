using MediatR;

namespace InternetBank.Application.Transaction.Commands.Transfer_Money;

public record TransferMoneyCommand(string Otp,
                                   double Amount,
                                   string UserId) : IRequest<string>;