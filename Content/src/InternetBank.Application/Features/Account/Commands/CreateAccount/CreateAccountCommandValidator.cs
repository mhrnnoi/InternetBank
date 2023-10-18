using FluentValidation;

namespace InternetBank.Application.Features.Account.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
    }
}