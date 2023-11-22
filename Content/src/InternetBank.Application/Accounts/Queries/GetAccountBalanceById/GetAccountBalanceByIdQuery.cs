using ErrorOr;
using MediatR;

namespace InternetBank.Application.Accounts.Queries.GetAccountBalanceById;

public record GetAccountBalanceByIdQuery(string UserId, string AccountId) : IRequest<ErrorOr<string>>;
