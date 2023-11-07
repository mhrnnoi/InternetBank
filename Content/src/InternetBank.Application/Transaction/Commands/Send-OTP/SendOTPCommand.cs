using MediatR;

namespace InternetBank.Application.Transaction.Commands.Send_OTP;

public record Send_OTPCommand(string CardNumber,
                              string Cvv2,
                              string ExpiryYear,
                              string ExpiryMonth,
                              double Amount,
                              string DestinationCardNumber,
                              string UserId) : IRequest<int>;