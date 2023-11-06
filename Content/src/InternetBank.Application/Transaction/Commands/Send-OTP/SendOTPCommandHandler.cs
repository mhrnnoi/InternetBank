using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MediatR;
using static InternetBank.Domain.Exceptions.DomainExceptions.Transaction;

namespace InternetBank.Application.Transaction.Commands.Send_OTP;

public class Send_OTPCommandHandler : IRequestHandler<Send_OTPCommand, int>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountRepository _accountRepository;

    public Send_OTPCommandHandler(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
        _accountRepository = accountRepository;
    }

    public async Task<int> Handle(Send_OTPCommand request, CancellationToken cancellationToken)
    {

        var sourceAccount = await _accountRepository.GetByCardNumber(request.CardNumber) ?? throw new IncorrectCardNumber();
        var destAccount = await _accountRepository.GetByCardNumber(request.DestinationCardNumber) ?? throw new IncorrectCardNumber();
        CheckAccounts(request, sourceAccount, destAccount);

        var transaction = Domain.Transactions.Transaction.CreateTransaction(request.Amount,
                                                                            request.DestinationCardNumber,
                                                                            request.CardNumber,
                                                                            request.CVV2,
                                                                            request.ExpiryYear,
                                                                            request.ExpiryMonth,
                                                                            _transactionRepository.SendOTP(),
                                                                            request.UserId);
        _transactionRepository.Add(transaction);
        await _unitOfWork.SaveChangesAsync();
        return transaction.Id;




    }

    private static void CheckAccounts(Send_OTPCommand request, Domain.Accounts.Account account, Domain.Accounts.Account destAccount)
    {

        if (account.UserId != request.UserId)
        {
            throw new AccountIsNotYours();
        }
        if (account.IsBlocked == true)
        {
            throw new AccountIsBlocked("source account is blocked");
        }
        if (destAccount.IsBlocked == true)
        {
            throw new AccountIsBlocked("destination account is blocked");
        }
        if (account.ExpiryYear != request.ExpiryYear || account.ExpiryMonth != request.ExpiryMonth)
        {
            throw new IncorrectExpiryDate();
        }
        if (account.CVV2 != request.CVV2)
        {
            throw new IncorrectCVV2();
        }
    }
}
