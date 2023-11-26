using ErrorOr;
using InternetBank.Application.Interfaces;
using InternetBank.Domain.Accounts.ValueObjects;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Transactions.Commands.Send_OTP;

public class Send_OTPCommandHandler : IRequestHandler<Send_OTPCommand, ErrorOr<string>>
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

    public async Task<ErrorOr<string>> Handle(Send_OTPCommand request,
                                              CancellationToken cancellationToken)
    {

        await _unitOfWork.SaveChangesAsync();
        var sourceCard = CardNumber.Create(request.CardNumber);
        var destinationCard = CardNumber.Create(request.DestinationCardNumber);
        var sourceAccount = await _accountRepository.GetByCardNumber(sourceCard);
        if (sourceAccount is null)
            return Errors.Transaction.SourceIncorrectCardNumber;

        if (sourceAccount.UserId != request.UserId)
            return Errors.Account.AccountIsNotYours;

        var user = await _identityService.GetByIdAsync(request.UserId);

        var destinationAccount = await _accountRepository.GetByCardNumber(destinationCard);
        if (destinationAccount is null)
            return Errors.Transaction.DestinationIncorrectCardNumber;

        var transactionOrError = sourceAccount.SendOTP(request.Amount, destinationCard);
        if (transactionOrError.IsError)
            return transactionOrError.Errors;

        // _transactionRepository.Add(transactionOrError.Value);
        await _unitOfWork.SaveChangesAsync();
        _transactionRepository.SendOTP(user.PhoneNumber, request.Amount, transactionOrError.Value.Otp);
        return transactionOrError.Value.Id.Value.ToString();

    }


}
