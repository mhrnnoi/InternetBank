using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MediatR;
using static InternetBank.Domain.Exceptions.DomainExceptions.Transaction;

namespace InternetBank.Application.Transaction.Commands.Transfer_Money;

public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand, string>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;


    public TransferMoneyCommandHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
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
            var res = transaction.TransferMoney(srcAcc, destAcc, request.UserId);
            await _unitOfWork.SaveChangesAsync();
            return res;
        }
        else
        {
            throw new IncorrectCardNumber();
        }


    }
}
