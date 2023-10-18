using MediatR;

namespace InternetBank.Application.Account.Queries.GetById;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDTO>
{
    public Task<AccountDTO> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
