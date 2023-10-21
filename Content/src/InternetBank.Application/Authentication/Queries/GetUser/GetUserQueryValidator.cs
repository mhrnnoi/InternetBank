using FluentValidation;

namespace InternetBank.Application.Authentication.Queries.GetUser;
public class GetUserByIdQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserByIdQueryValidator()
    {
    }
}