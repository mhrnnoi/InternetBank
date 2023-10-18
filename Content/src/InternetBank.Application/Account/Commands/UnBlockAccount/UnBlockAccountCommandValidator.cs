using FluentValidation;

namespace InternetBank.Application.Account.Commands.UnBlockAccount;

public class UnBlockAccountCommandValidator : AbstractValidator<bool>
{
    public UnBlockAccountCommandValidator()
    {
    }
}
