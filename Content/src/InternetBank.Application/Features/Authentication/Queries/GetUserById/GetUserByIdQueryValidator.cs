using FluentValidation;

namespace InternetBank.Application.Features.Authentication.Queries.GetUserById;
public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
    }
}