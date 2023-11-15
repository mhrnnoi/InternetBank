// using ErrorOr;
// using InternetBank.Application.Interfaces;
// using InternetBank.Domain.Accounts.ValueObjects;
// using InternetBank.Domain.Common.Errors;
// using InternetBank.Domain.Exceptions.Transaction;
// using InternetBank.Domain.Interfaces.UOF;
// using InternetBank.Domain.Repositories;
// using InternetBank.Domain.Transactions.Entities;
// using MediatR;

// namespace InternetBank.Application.Transactions.Commands.Send_OTP;

// public class Send_OTPCommandHandler : IRequestHandler<Send_OTPCommand, ErrorOr<string>>
// {
//     private readonly ITransactionRepository _transactionRepository;
//     private readonly IUnitOfWork _unitOfWork;
//     private readonly IAccountRepository _accountRepository;
//     private readonly IIdentityService _identityService;

//     public Send_OTPCommandHandler(ITransactionRepository transactionRepository,
//                                   IUnitOfWork unitOfWork,
//                                   IAccountRepository accountRepository,
//                                   IIdentityService identityService)
//     {
//         _transactionRepository = transactionRepository;
//         _unitOfWork = unitOfWork;
//         _accountRepository = accountRepository;
//         _identityService = identityService;
//     }

//     public async Task<ErrorOr<string>> Handle(Send_OTPCommand request,
//                                               CancellationToken cancellationToken)
//     {

//         var errors = new List<Error>();
//         var sourceAccount = await _accountRepository.GetByCardNumber(request.SourceCardNumber);
//         if (sourceAccount is null)
//             errors.Add(Errors.Transaction.SourceIncorrectCardNumber);

//         var destinationAccount = await _accountRepository.GetByCardNumber(request.DestCardNumber);
//         if (destinationAccount is null)
//             errors.Add(Errors.Transaction.DestinationIncorrectCardNumber);

//         if (errors.Any())
//             return errors;

//         var transaction = sourceAccount!.FindTransaction(request.Amount, request.DestCardNumber);
//         if (transaction is null)
//         {
//             var transactionRes = Transaction.CreateTransaction(request.Amount, request.DestCardNumber);
//             if (transactionRes.IsError)
//             {
//                 return transactionRes.Errors;
//             }
//             transaction = transactionRes.Value;
//             sourceAccount.AddTransaction(transaction);
//         }

//         var user = await _identityService.GetByIdAsync(request.UserId);

//         var otp = _transactionRepository.SendOTP(user.PhoneNumber, request.Amount, Otp.GenerateOTP());
//         transaction.SetOtp(otp);
//         _transactionRepository.Add(transaction);
//         await _unitOfWork.SaveChangesAsync();
//         return transaction.Id.Value.ToString();
//     }


// }
