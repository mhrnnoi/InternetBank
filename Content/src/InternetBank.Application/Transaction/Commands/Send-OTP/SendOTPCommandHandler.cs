using InternetBank.Application.Interfaces;
using InternetBank.Domain.Exceptions.Transaction;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using InternetBank.Domain.Transactions.Entities;
using MediatR;

namespace InternetBank.Application.Transactions.Commands.Send_OTP;

public class Send_OTPCommandHandler : IRequestHandler<Send_OTPCommand, string>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountRepository _accountRepository;
    private readonly IIdentityService _identityService;

    public Send_OTPCommandHandler(ITransactionRepository transactionRepository,
                                  IUnitOfWork unitOfWork,
                                  IAccountRepository accountRepository,
                                  IIdentityService identityService)
    {
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
        _accountRepository = accountRepository;
        _identityService = identityService;
    }

    public async Task<string> Handle(Send_OTPCommand request,
                                  CancellationToken cancellationToken)
    {

        var sourceAccount = await _accountRepository.GetByCardNumber(request.CardNumber) ?? throw new IncorrectCardNumber();
        var destAccount = await _accountRepository.GetByCardNumber(request.DestinationCardNumber) ?? throw new IncorrectCardNumber();
        var user = await _identityService.GetByIdAsync(sourceAccount.UserId);

        var transaction = Transaction.CreateTransaction(sourceAccount,
                                                        destAccount,
                                                        request.ExpiryYear,
                                                        request.ExpiryMonth,
                                                        request.Amount,
                                                        request.Cvv2,
                                                        request.UserId);

        transaction.Value.SetOtp(_transactionRepository.SendOTP(user.PhoneNumber,
                                                          request.Amount));
        _transactionRepository.Add(transaction.Value);
        await _unitOfWork.SaveChangesAsync();
        return transaction.Value.Id;
    }


}
