using FluentValidation;

namespace InternetBank.Application.Accounts.Commands.UnBlockAccount;

public class UnBlockAccountCommandValidator : AbstractValidator<UnBlockAccountCommand>
{
    public UnBlockAccountCommandValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty()
                                 .NotNull();
    }
}
