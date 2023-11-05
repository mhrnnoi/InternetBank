using FluentValidation;

namespace InternetBank.Application.Account.Commands.BlockAccount;

public class BlockAccountCommandValidator : AbstractValidator<BlockAccountCommand>
{
    public BlockAccountCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
