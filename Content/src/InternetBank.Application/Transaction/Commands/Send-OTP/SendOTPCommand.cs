using MediatR;

namespace InternetBank.Application.Transaction.Commands.Send_OTP;

public record Send_OTPCommand(string CardNumber,
                              string CVV2,
                              DateTime ExpiryDate,
                              double Amount,
                              string DestinationCardNumber,
                              string UserId) : IRequest<int>;