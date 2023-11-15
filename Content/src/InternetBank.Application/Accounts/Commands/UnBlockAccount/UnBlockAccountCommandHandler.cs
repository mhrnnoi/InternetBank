// using ErrorOr;
// using InternetBank.Domain.Accounts.Entities;
// using InternetBank.Domain.Common.Errors;
// using InternetBank.Domain.Exceptions.User;
// using InternetBank.Domain.Interfaces.UOF;
// using InternetBank.Domain.Repositories;
// using MediatR;

// namespace InternetBank.Application.Accounts.Commands.UnBlockAccount;

// public class UnBlockAccountCommandHandler : IRequestHandler<UnBlockAccountCommand, ErrorOr<bool>>
// {
//     private readonly IAccountRepository _accountRepository;
//     private readonly IUnitOfWork _unitOfWork;

//     public UnBlockAccountCommandHandler(IUnitOfWork unitOfWork, IAccountRepository accountRepository)
//     {
//         _unitOfWork = unitOfWork;
//         _accountRepository = accountRepository;
//     }

//     public async Task<ErrorOr<bool>> Handle(UnBlockAccountCommand request, CancellationToken cancellationToken)
//     {
//         var acc = await _accountRepository.GetById(request.Id);
//         if (acc is null)
//             return Errors.Account.InvalidAccountType;
//         if (acc.UserId != request.UserId)
//         {
//             return Errors.Account.AccountIsNotYours;
            
//         }
//         acc.BlockAccount();
//         await _unitOfWork.SaveChangesAsync();
//         return true;
//     }
// }
