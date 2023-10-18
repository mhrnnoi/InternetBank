using FluentValidation;

namespace InternetBank.Application.Features.Account.Commands.BlockAccount;

public class BlockAccountCommandValidator : AbstractValidator<bool>
{
    public BlockAccountCommandValidator()
    {
    }
}
