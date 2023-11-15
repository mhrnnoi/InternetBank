// using ErrorOr;
// using InternetBank.Domain.Accounts.ValueObjects;
// using InternetBank.Domain.Common.Errors;
// using InternetBank.Domain.Exceptions.Transaction;
// using InternetBank.Domain.Interfaces.UOF;
// using InternetBank.Domain.Repositories;
// using MediatR;

// namespace InternetBank.Application.Transactions.Commands.Transfer_Money;

// public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand, ErrorOr<string>>
// {
//     private readonly ITransactionRepository _transactionRepository;
//     private readonly IAccountRepository _accountRepository;
//     private readonly IUnitOfWork _unitOfWork;


//     public TransferMoneyCommandHandler(ITransactionRepository transactionRepository,
//                                        IAccountRepository accountRepository,
//                                        IUnitOfWork unitOfWork)
//     {
//         _transactionRepository = transactionRepository;
//         _accountRepository = accountRepository;
//         _unitOfWork = unitOfWork;
//     }

//     public async Task<ErrorOr<string>> Handle(TransferMoneyCommand request,
//                                      CancellationToken cancellationToken)
//     {
//         var errors = new List<Error>();
//         var sourceAccount = await _accountRepository.GetByCardNumber(request.SourceCardNumber);
//         if (sourceAccount is null)
//             errors.Add(Errors.Transaction.SourceIncorrectCardNumber);

//         var destinationAccount = await _accountRepository.GetByCardNumber(request.DestCardNumber);
//         if (sourceAccount is null)
//             errors.Add(Errors.Transaction.DestinationIncorrectCardNumber);

//         if (errors.Any())
//             return errors;

//         var otp = Otp.ConvertToOTP(request.Otp);

//         var transaction = sourceAccount!.FindTransaction(request.Amount, request.DestCardNumber);
//         if (transaction is null)
//             return Errors.Account.IncorrectPass;

//         var desc = sourceAccount.TransferMoney(request.Amount,
//                                                request.Cvv2,
//                                                request.ExpiryDate,
//                                                otp,
//                                                destinationAccount!,
//                                                transaction);

//         return desc;

//     }
// }
