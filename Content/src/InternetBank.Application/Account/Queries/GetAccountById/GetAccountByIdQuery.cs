using InternetBank.Application.Account.Queries.GetAccountById;
using MediatR;

namespace InternetBank.Application.Account.Queries.GetById;

public record GetAccountByIdQuery(int Id, string UserId) : IRequest<AccountDTO>;


