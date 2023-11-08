using MediatR;

namespace InternetBank.Application.Accounts.Queries.GetAccountById;

public record GetAccountByIdQuery(string Id,
                                  string UserId) : IRequest<AccountDTO>;


