using System.Data;
using FluentValidation;

namespace InternetBank.Application.Features.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("incorrect email address format");
        RuleFor(x => x.PhoneNumber.Select(x => char.IsNumber(x)).Count()).NotEmpty().GreaterThanOrEqualTo(10).LessThanOrEqualTo(16)
                                    .WithMessage("its not correct phone number");
        RuleFor(x => x.FirstName.Length).NotEmpty().GreaterThanOrEqualTo(2).LessThanOrEqualTo(15)
        .WithMessage("plz enter valid name ");
        RuleFor(x => x.LastName.Length).NotEmpty().GreaterThanOrEqualTo(2).LessThanOrEqualTo(15)
        .WithMessage("plz enter valid name ");
        RuleFor(x => x.BirthDate).NotEmpty().Must(x => x.GetType() == typeof(DateTime))
                                    .WithMessage("enter valid date time");
        RuleFor(x => x.NationalCode.Length).NotEmpty().Equal(10)
                                    .WithMessage("enter valid national code");

    }
}
