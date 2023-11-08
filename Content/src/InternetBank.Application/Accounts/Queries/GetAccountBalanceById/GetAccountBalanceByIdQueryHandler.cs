using InternetBank.Domain.Exceptions.User;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Accounts.Queries.GetAccountBalanceById;

public class GetAccountBalanceByIdQueryHandler : IRequestHandler<GetAccountBalanceByIdQuery, BalanceDTO>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountBalanceByIdQueryHandler(IAccountRepository accountRepository,
                                             IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
    }

    public async Task<BalanceDTO> Handle(GetAccountBalanceByIdQuery request,
                                     CancellationToken cancellationToken)
    {
        var acc = await _accountRepository.GetById(request.Id,
                                                   request.UserId) ?? throw new NotFoundAccountById();
        return new BalanceDTO(acc.Amount,
                              acc.Id,
                              acc.AccountNumber);
    }
}
