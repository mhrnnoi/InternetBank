using MediatR;

namespace InternetBank.Application.Account.Queries.GetById;

public record GetAccountByIdQuery(int Id) : IRequest<AccountDTO>;

public class AccountDTO
{
}