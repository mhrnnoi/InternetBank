using ErrorOr;
using InternetBank.Domain.Accounts.ValueObjects;
using MediatR;

namespace InternetBank.Application.Transactions.Commands.Send_OTP;

public record Send_OTPCommand(
                              double Amount,
                              string UserId) : IRequest<ErrorOr<string>>;