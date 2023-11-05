using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Transaction.Commands.Transfer_Money;

public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand, bool>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;

    public TransferMoneyCommandHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
    }

    public Task<bool> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
    {
        _transactionRepository
    }
}
