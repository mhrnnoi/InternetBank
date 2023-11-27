using ErrorOr;
using MediatR;

namespace InternetBank.Application.Transactions.Commands.Transfer_Money;

public record TransferMoneyCommand(double Amount,
                                   string Cvv2,
                                   string ExpiryYear,
                                   string ExpiryMonth,
                                   string Otp,
                                   string DestinationCardNumber,
                                   string SourceCardNumber,
                                   string UserId) : IRequest<ErrorOr<string>>;
