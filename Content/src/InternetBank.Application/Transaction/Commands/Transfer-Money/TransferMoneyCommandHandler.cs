using InternetBank.Domain.Repositories;
using MediatR;
using static InternetBank.Domain.Exceptions.DomainExceptions.Transaction;

namespace InternetBank.Application.Transaction.Commands.Transfer_Money;

public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand, string>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;

    public TransferMoneyCommandHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
    }

    public async Task<string> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByOTP(request.OTP, request.Amount);
        if (transaction is null)
        {
            throw new WrongOTP();
        }

        var srcAcc = await _accountRepository.GetByCardNumber(transaction.SourceCardNumber);
        var destAcc = await _accountRepository.GetByCardNumber(transaction.DestinationCardNumber);

        if (srcAcc is not null && destAcc is not null)
        {
           return transaction.TransferMoney(srcAcc, destAcc, request.UserId);
        }
        else
        {
            throw new IncorrectCardNumber();
        }


    }
}
