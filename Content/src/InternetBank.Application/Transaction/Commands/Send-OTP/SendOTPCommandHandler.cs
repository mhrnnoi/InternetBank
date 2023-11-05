using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MediatR;

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

        var account = await _accountRepository.GetByCardNumber(request.CardNumber);
        if (account is not null && account.CVV2 == request.CVV2 && account.IsBlocked == false && account.UserId == request.UserId && account.ExpiryDate == request.ExpiryDate)
        {
            var transaction = Domain.Transactions.Transaction.CreateTransaction(request.Amount,
                                                                    request.DestinationCardNumber,
                                                                    request.CardNumber,
                                                                    request.CVV2,
                                                                    request.ExpiryDate,
                                                                    _transactionRepository.SendOTP(),
                                                                    request.UserId);
            _transactionRepository.Add(transaction);
            await _unitOfWork.SaveChangesAsync();
            return transaction.Id;
        }
        throw new Exception();

        

    }
}
