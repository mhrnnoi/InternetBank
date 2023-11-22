using FluentValidation;

namespace InternetBank.Application.Accounts.Commands.BlockAccount;

public class BlockAccountCommandValidator : AbstractValidator<BlockAccountCommand>
{
    public BlockAccountCommandValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty()
                                 .NotNull();
    }
}
