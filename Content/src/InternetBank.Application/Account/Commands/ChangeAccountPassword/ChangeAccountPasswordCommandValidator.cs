using FluentValidation;

namespace InternetBank.Application.Account.Commands.ChangeAccountPassword;

public class ChangeAccountPasswordCommandValidator : AbstractValidator<ChangeAccountPasswordCommand>
{
    public ChangeAccountPasswordCommandValidator()
    {
    }
}
