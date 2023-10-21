using FluentValidation;

namespace InternetBank.Application.Authentication.Queries.GetUsers;
public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {
    }
}