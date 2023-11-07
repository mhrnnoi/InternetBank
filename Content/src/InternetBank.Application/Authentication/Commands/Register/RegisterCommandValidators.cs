using System.Data;
using FluentValidation;

namespace InternetBank.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress()
                             .NotEmpty()
                             .WithMessage("incorrect email address format");

        RuleFor(x => x.Password).NotEmpty()
                                .WithMessage("plz enter password");

        RuleFor(x => x.Username).NotEmpty()
                                .WithMessage("plz enter username");

        RuleFor(x => x.PhoneNumber.Select(x => char.IsNumber(x))
                                  .Count()).NotEmpty()
                                           .GreaterThanOrEqualTo(10)
                                           .LessThanOrEqualTo(16)
                                           .WithMessage("its not correct phone number")
                                           .WithName("phone number lenght");

        RuleFor(x => x.FirstName.Length).NotEmpty()
                                        .GreaterThanOrEqualTo(2)
                                        .LessThanOrEqualTo(15)
                                        .WithMessage("plz enter valid first name ");

        RuleFor(x => x.LastName.Length).NotEmpty()
                                       .GreaterThanOrEqualTo(2)
                                       .LessThanOrEqualTo(15)
                                       .WithMessage("plz enter valid last name ");

        RuleFor(x => x.BirthDate).NotEmpty()
                                 .Must(x => x.GetType() == typeof(DateOnly))
                                 .WithMessage("enter valid birth date");

        RuleFor(x => x.NationalCode.Length).NotEmpty()
                                           .Equal(10)
                                           .WithMessage("enter valid national code");

    }
}
