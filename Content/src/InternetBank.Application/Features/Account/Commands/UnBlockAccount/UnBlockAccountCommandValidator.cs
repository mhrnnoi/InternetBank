using FluentValidation;

namespace InternetBank.Application.Features.Account.Commands.UnBlockAccount;

public class UnBlockAccountCommandValidator : AbstractValidator<bool>
{
    public UnBlockAccountCommandValidator()
    {
    }
}
