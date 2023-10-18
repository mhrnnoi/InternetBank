using MediatR;

namespace InternetBank.Application.Features.Account.Queries.GetById;

public record GetAccountByIdQuery(int Id) : IRequest<AccountDTO>;

public class AccountDTO
{
}