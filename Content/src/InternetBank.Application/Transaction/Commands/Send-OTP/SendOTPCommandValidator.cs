using FluentValidation;

namespace InternetBank.Application.Transaction.Commands.Send_OTP;

public class Send_OTPCommandValidator : AbstractValidator<Send_OTPCommand>
{
    public Send_OTPCommandValidator()
    {
        RuleFor(x => x.CVV2)
                            .Must(x => x.All(x => char.IsDigit(x)) && x.Length == 4)
                            .WithMessage("CVV2 should have 4 numeric character");
        RuleFor(x => x.Amount)
                            .GreaterThanOrEqualTo(1000)
                            .LessThanOrEqualTo(5000000)
                            .WithMessage("amount shoul be greater than 1k and less then 5 milion");

        // RuleFor(x => x.ExpiryDate)
        //                 .GreaterThanOrEqualTo(DateTime.UtcNow)
        //                 .WithMessage("card is expired");

        // RuleFor(x => x.CardNumber)
        //                     .Must(x => x.All(x => char.IsDigit(x)) && x.Length == 16)
        //                     .WithMessage("card number should have 16 numeric character");
        // RuleFor(x => x.DestinationCardNumber)
        //                     .Must(x => x.All(x => char.IsDigit(x)) && x.Length == 16)
        //                     .WithMessage("destination card number should have 16 numeric character");
        
    }
}
