using FluentValidation;

namespace InternetBank.Application.Account.Queries.GetById;

public class GetAccountByIdQueryValidator : AbstractValidator<GetAccountByIdQuery>
{
    public GetAccountByIdQueryValidator()
    {
    }
}
