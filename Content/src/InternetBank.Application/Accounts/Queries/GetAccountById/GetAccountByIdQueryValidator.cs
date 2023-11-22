using FluentValidation;

namespace InternetBank.Application.Accounts.Queries.GetAccountById;

public class GetAccountByIdQueryValidator : AbstractValidator<GetAccountByIdQuery>
{
    public GetAccountByIdQueryValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty()
                                 .NotNull();
    }
}
