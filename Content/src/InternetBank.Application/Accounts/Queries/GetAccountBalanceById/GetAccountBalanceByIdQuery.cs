using ErrorOr;
using MediatR;

namespace InternetBank.Application.Accounts.Queries.GetAccountBalanceById;

public record GetAccountBalanceByIdQuery(string Id,
                                         string UserId) : IRequest<ErrorOr<BalanceDTO>>;
