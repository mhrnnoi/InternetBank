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

    public async Task<bool> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
    {
        var transaction =  await _transactionRepository.GetByOTP(request.OTP, request.Amount);
        if (transaction is not null && (DateTime.UtcNow - transaction.CreatedDateTime).Minutes <= 2 && transaction.IsSuccess == false)
        {
            var srcAcc =  await _accountRepository.GetByCardNumber(transaction.SourceCardNumber);
            var destAcc = await _accountRepository.GetByCardNumber(transaction.DestinationCardNumber);
            srcAcc.Withdrawl(transaction.Amount);
            destAcc.Deposit(transaction.Amount);
            transaction.IsSuccess = true;
            return true;

        }
        throw new Exception();

    }
}
