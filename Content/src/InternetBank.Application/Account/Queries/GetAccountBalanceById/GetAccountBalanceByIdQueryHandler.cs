using InternetBank.Domain.Exceptions;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Account.Queries.GetAccountBalanceById;

public class GetAccountBalanceByIdQueryHandler : IRequestHandler<GetAccountBalanceByIdQuery, string>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountBalanceByIdQueryHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
    }

    public async Task<string> Handle(GetAccountBalanceByIdQuery request, CancellationToken cancellationToken)
    {
        var acc = await _accountRepository.GetById(request.Id, request.UserId) ?? throw new DomainExceptions.User.NotFoundAccountById();
        return acc.Balance();
    }
}
