using FluentValidation;

namespace InternetBank.Application.Transaction.Commands.Transfer_Money;

public class TransferMoneyCommandValidator : AbstractValidator<TransferMoneyCommand>
{
    public TransferMoneyCommandValidator()
    {
    }
}
