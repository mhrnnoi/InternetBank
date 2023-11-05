using MediatR;

namespace InternetBank.Application.Transaction.Commands.Transfer_Money;

public record TransferMoneyCommand(string OTP, double Amount, string UserId) : IRequest<bool>;