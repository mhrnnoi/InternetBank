using MediatR;

namespace InternetBank.Application.Accounts.Queries.GetAllAccounts;

public record GetAllAccountsQuery(string UserId) : IRequest<List<AllAccountsDTO>>;

