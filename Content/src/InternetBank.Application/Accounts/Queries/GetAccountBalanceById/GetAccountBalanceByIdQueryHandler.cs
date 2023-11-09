using ErrorOr;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Exceptions.User;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Accounts.Queries.GetAccountBalanceById;

public class GetAccountBalanceByIdQueryHandler : IRequestHandler<GetAccountBalanceByIdQuery, ErrorOr<BalanceDTO>>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountBalanceByIdQueryHandler(IAccountRepository accountRepository,
                                             IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<BalanceDTO>> Handle(GetAccountBalanceByIdQuery request,
                                     CancellationToken cancellationToken)
    {
        var acc = await _accountRepository.GetById(request.Id,
                                                   request.UserId);
        if (acc is null)
            return Errors.User.NotFoundAccountById;
            
        return new BalanceDTO(acc.Amount,
                              acc.Id,
                              acc.AccountNumber);
    }
}
