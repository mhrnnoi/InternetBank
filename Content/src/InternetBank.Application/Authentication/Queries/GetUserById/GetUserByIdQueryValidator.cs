using FluentValidation;

namespace InternetBank.Application.Authentication.Queries.GetUserById;
public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
    }
}