using ErrorOr;
using InternetBank.Domain.Accounts.ValueObjects;
using MediatR;

namespace InternetBank.Application.Transactions.Commands.Transfer_Money;

public record TransferMoneyCommand(string Otp,
                                   double Amount,
                                   string UserId): IRequest<ErrorOr<string>>;
                                //    CardNumber SourceCardNumber,
                                //    CardNumber DestCardNumber,
                                //    Cvv2 Cvv2,
                                //    ExpiryDate ExpiryDate) 