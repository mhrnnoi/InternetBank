using FluentValidation;

namespace InternetBank.Application.Transactions.Commands.Send_OTP;

public class Send_OTPCommandValidator : AbstractValidator<Send_OTPCommand>
{
    public Send_OTPCommandValidator()
    {
        RuleFor(x => x.Cvv2)
                            .Must(x => x.All(x => char.IsDigit(x)) && x.Length == 4)
                            .WithMessage("CVV2 should have 4 numeric character");
        RuleFor(x => x.Amount)
                            .GreaterThanOrEqualTo(1000)
                            .LessThanOrEqualTo(5000000)
                            .WithMessage("amount shoul be greater than 1k and less then 5 milion");

        RuleFor(x => x.ExpiryYear).Must((model, field, context) => Convert.ToDateTime(model.ExpiryYear + "/" + model.ExpiryMonth) >= DateTime.UtcNow)
                        .WithMessage("card is expired");

        RuleFor(x => x.CardNumber)
                            .Must(x => x.Where(x => char.IsDigit(x) == true).Count() == 16)
                            .WithMessage("card number should have 16 numeric character");
        RuleFor(x => x.DestinationCardNumber)
                            .Must(x => x.Where(x => char.IsDigit(x) == true).Count() == 16)
                            .WithMessage("destination card number should have 16 numeric character");

    }
}
