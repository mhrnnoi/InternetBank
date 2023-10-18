using FluentValidation;

namespace InternetBank.Application.Account.Commands.BlockAccount;

public class BlockAccountCommandValidator : AbstractValidator<bool>
{
    public BlockAccountCommandValidator()
    {
    }
}
