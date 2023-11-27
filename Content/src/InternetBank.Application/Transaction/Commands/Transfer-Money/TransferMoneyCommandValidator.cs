using FluentValidation;

namespace InternetBank.Application.Transactions.Commands.Transfer_Money;

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
        RuleFor(x => x.ExpiryYear).Must((model, field, context) => Convert.ToDateTime(model.ExpiryYear + "/" + model.ExpiryMonth) >= DateTime.UtcNow)
                        .WithMessage("card is expired");

        RuleFor(x => x.SourceCardNumber)
                            .Must(x => x.Where(x => char.IsDigit(x) == true).Count() == 16)
                            .WithMessage("card number should have 16 numeric character");
        RuleFor(x => x.DestinationCardNumber)
                            .Must(x => x.Where(x => char.IsDigit(x) == true).Count() == 16)
                            .WithMessage("destination card number should have 16 numeric character");
    }
}
