using ErrorOr;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Exceptions.Transaction;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Transactions.Commands.Transfer_Money;

public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand, ErrorOr<string>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;


    public TransferMoneyCommandHandler(ITransactionRepository transactionRepository,
                                       IAccountRepository accountRepository,
                                       IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<string>> Handle(TransferMoneyCommand request,
                                     CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByOTP(request.Otp,
                                                                request.Amount,
                                                                request.UserId) ?? throw new WrongOTP();

        var srcAcc = await _accountRepository.GetById(transaction.AccountId,
                                                      request.UserId);

        var destAcc = await _accountRepository.GetByCardNumber(transaction.DestinationCardNumber);

        if (srcAcc is not null && destAcc is not null)
        {
            var res = transaction.TransferMoney(srcAcc,
                                                destAcc,
                                                request.UserId);
            if (res.IsError)
                return res.Errors;
            await _unitOfWork.SaveChangesAsync();
            return res.Value;


        }
        else if (srcAcc is null && destAcc is not null)
        {
            return Errors.Transaction.SourceIncorrectCardNumber;
        }
        else if (srcAcc is not null && destAcc is null)
            return Errors.Transaction.DestinationAccountIsBlocked;

        return new List<Error>()
        {
            Errors.Transaction.DestinationAccountIsBlocked,
            Errors.Transaction.SourceIncorrectCardNumber
        };

    }
}
