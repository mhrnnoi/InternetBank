using FluentValidation;

namespace InternetBank.Application.Account.Commands.UnBlockAccount;

public class UnBlockAccountCommandValidator : AbstractValidator<UnBlockAccountCommand>
{
    public UnBlockAccountCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty()
                          .NotNull();
    }
}
