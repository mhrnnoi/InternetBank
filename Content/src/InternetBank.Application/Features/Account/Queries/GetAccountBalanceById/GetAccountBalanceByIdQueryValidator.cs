using FluentValidation;

namespace InternetBank.Application.Features.Account.Queries.GetAccountBalanceById;

public class GetAccountBalanceByIdQueryValidator : AbstractValidator<GetAccountBalanceByIdQuery>
{
    public GetAccountBalanceByIdQueryValidator()
    {
    }
}
