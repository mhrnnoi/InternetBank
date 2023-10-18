using MediatR;

namespace InternetBank.Application.Features.Account.Queries.GetById;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDTO>
{
    public Task<AccountDTO> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
