using System.Data;
using FluentValidation;

namespace InternetBank.Application.Account.Commands.ChangeAccountPassword;

public class ChangeAccountPasswordCommandValidator : AbstractValidator<ChangeAccountPasswordCommand>
{
    public ChangeAccountPasswordCommandValidator()
    {
        RuleFor(x => x.NewPassword).Must(x => x.Length == 6 &&
                                                x.All(x => char.IsDigit(x)))
                                                    .WithMessage("password should have 6 numeric characters");
        RuleFor(x => x.RepeatNewPassword).Must((model, field, context) => model.NewPassword == field)
                                         .WithMessage("new pass and repeat should equal");
    }
}
