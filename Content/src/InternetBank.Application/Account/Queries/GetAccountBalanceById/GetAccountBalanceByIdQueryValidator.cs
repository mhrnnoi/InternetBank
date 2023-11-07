using FluentValidation;

namespace InternetBank.Application.Account.Queries.GetAccountBalanceById;

public class GetAccountBalanceByIdQueryValidator : AbstractValidator<GetAccountBalanceByIdQuery>
{
    public GetAccountBalanceByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty()
                          .NotNull();
    }
}
