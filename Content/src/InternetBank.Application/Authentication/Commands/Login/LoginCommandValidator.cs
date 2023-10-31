using FluentValidation;

namespace InternetBank.Application.Authentication.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress()
                             .NotEmpty()
                             .WithMessage("incorrect email address format");
        RuleFor(x => x.Password)
                             .NotEmpty()
                             .WithMessage("plz enter password");

    }
}