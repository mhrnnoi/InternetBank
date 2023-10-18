using FluentValidation;

namespace InternetBank.Application.Features.Account.Queries.GetById;

public class GetAccountByIdQueryValidator : AbstractValidator<GetAccountByIdQuery>
{
    public GetAccountByIdQueryValidator()
    {
    }
}
