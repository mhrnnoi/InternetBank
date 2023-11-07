using FluentValidation;

namespace InternetBank.Application.Account.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Amount).GreaterThanOrEqualTo(10000)
                              .WithMessage("the amount of creating account should greater or equal to 10k");
        RuleFor(x => x.AccountType).GreaterThanOrEqualTo(1)
                                    .LessThanOrEqualTo(2)
                                    .WithMessage("the account type should be 1 (for saving) or 2 (for checking)");
    }
}