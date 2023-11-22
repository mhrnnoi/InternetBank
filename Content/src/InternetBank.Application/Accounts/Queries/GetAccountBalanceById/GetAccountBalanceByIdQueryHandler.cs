using ErrorOr;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Accounts.Queries.GetAccountBalanceById;

public class GetAccountBalanceByIdQueryHandler : IRequestHandler<GetAccountBalanceByIdQuery, ErrorOr<string>>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountBalanceByIdQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<string>> Handle(GetAccountBalanceByIdQuery request,
                                                  CancellationToken cancellationToken)
    {
        var acc = await _accountRepository.GetById(request.AccountId);
        if (acc is null || acc.UserId != request.UserId)
            return Errors.User.NotFoundAccountById;
        
        return acc.Balance();
 
    }
}
