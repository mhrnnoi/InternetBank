using FluentValidation;

namespace InternetBank.Application.Transaction.Commands.Transfer_Money;

public class TransferMoneyCommandValidator : AbstractValidator<TransferMoneyCommand>
{
    public TransferMoneyCommandValidator()
    {
        RuleFor(x => x.Otp)
                      .Must(x => x.All(x => char.IsDigit(x)) && x.Length == 5)
                      .WithMessage("otp should have 5 numeric character");
        RuleFor(x => x.Amount)
                      .GreaterThanOrEqualTo(1000)
                      .LessThanOrEqualTo(5000000)
                      .WithMessage("amount shoul be greater than 1k and less then 5 milion");
    }
}
