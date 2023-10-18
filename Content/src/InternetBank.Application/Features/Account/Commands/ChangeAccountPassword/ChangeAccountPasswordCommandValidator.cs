using FluentValidation;

namespace InternetBank.Application.Features.Account.Commands.ChangeAccountPassword;

public class ChangeAccountPasswordCommandValidator : AbstractValidator<ChangeAccountPasswordCommand>
{
    public ChangeAccountPasswordCommandValidator()
    {
    }
}
