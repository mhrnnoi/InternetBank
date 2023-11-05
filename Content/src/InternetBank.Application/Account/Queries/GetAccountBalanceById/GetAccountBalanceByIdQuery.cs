using MediatR;

namespace InternetBank.Application.Account.Queries.GetAccountBalanceById;

public record GetAccountBalanceByIdQuery(int Id, string UserId) : IRequest<string>;
