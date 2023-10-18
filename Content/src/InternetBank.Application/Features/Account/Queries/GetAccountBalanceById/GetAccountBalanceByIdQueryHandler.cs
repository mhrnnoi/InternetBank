using MediatR;

namespace InternetBank.Application.Features.Account.Queries.GetAccountBalanceById;

public class GetAccountBalanceByIdQueryHandler : IRequestHandler<GetAccountBalanceByIdQuery, AccountBalanceDTO>
{
    Task<AccountBalanceDTO> IRequestHandler<GetAccountBalanceByIdQuery, AccountBalanceDTO>.Handle(GetAccountBalanceByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
