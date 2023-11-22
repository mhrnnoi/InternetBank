using InternetBank.Application.Accounts.Queries.GetAccountById;
using MediatR;

namespace InternetBank.Application.Accounts.Queries.GetAllAccounts;

public record GetUserAllAccountsQuery(string UserId) : IRequest<List<AccountDTO>>;

