using MediatR;

namespace InternetBank.Application.Account.Queries.GetAllAccounts;

public record GetAllAccountsQuery(string UserId) : IRequest<List<AllAccountsDTO>>;

public record AllAccountsDTO(string Number,
                             int Id,
                             string CardNumber);