using InternetBank.Domain.Accounts;
using MediatR;

namespace InternetBank.Application.Account.Queries.GetById;

public record GetAccountByIdQuery(int Id, string UserId) : IRequest<AccountDTO>;

public record AccountDTO(string Number,
                         string CardNumber,
                         string CVV2,
                         DateTime ExpiryDate,
                         string Password,
                         int Id,
                         AccountTypes Type);
