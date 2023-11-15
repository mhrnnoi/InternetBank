using ErrorOr;
using InternetBank.Domain.Accounts.ValueObjects;
using MediatR;

namespace InternetBank.Application.Accounts.Queries.GetAccountBalanceById;

public record GetAccountBalanceByIdQuery(string UserId) : IRequest<ErrorOr<BalanceDTO>>;
