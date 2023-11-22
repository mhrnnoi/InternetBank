using ErrorOr;
using MediatR;

namespace InternetBank.Application.Accounts.Queries.GetAccountById;

public record GetAccountByIdQuery(string UserId, string AccountId) : IRequest<ErrorOr<AccountDTO>>;


