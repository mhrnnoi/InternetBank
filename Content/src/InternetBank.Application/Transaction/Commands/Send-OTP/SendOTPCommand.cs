using ErrorOr;
using MediatR;

namespace InternetBank.Application.Transactions.Commands.Send_OTP;

public record Send_OTPCommand(string CardNumber,
                              double Amount,
                              string DestinationCardNumber,
                              string UserId) : IRequest<ErrorOr<bool>>;