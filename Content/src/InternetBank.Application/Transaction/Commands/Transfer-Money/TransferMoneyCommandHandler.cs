using ErrorOr;
using InternetBank.Domain.Accounts.ValueObjects;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Transactions.Commands.Transfer_Money;

public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand, ErrorOr<string>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;


    public TransferMoneyCommandHandler(IAccountRepository accountRepository,
                                       IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<string>> Handle(TransferMoneyCommand request,
                                              CancellationToken cancellationToken)
    {
        var sourceCard = CardNumber.Create(request.SourceCardNumber);
        var destinationCard = CardNumber.Create(request.DestinationCardNumber);
        var sourceAccount = await _accountRepository.GetByCardNumber(sourceCard);

        if (sourceAccount is null)
            return Errors.Transaction.SourceIncorrectCardNumber;

        if (sourceAccount.UserId != request.UserId)
            return Errors.Account.AccountIsNotYours;

        var destinationAccount = await _accountRepository.GetByCardNumber(destinationCard);

        if (destinationAccount is null)
            return Errors.Transaction.DestinationIncorrectCardNumber;

        var otp = Otp.ConvertToOTP(request.Otp);

        var cvv2 = Cvv2.Create(request.Cvv2);

        var expiryDate = Convert.ToDateTime(request.ExpiryYear + "/" + request.ExpiryMonth);

        var transaction = sourceAccount.TransferMoney(request.Amount,
                                                      cvv2,
                                                      expiryDate,
                                                      otp,
                                                      destinationAccount);

        if (transaction.IsError)
            return transaction.Errors;

        await _unitOfWork.SaveChangesAsync();

        return transaction.Value;

    }
}
