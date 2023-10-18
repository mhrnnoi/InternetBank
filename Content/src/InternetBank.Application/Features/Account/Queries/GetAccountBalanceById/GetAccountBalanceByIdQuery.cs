using MediatR;

namespace InternetBank.Application.Features.Account.Queries.GetAccountBalanceById;

public record GetAccountBalanceByIdQuery(int Id) : IRequest<AccountBalanceDTO>;

public class AccountBalanceDTO
{
}