// using ErrorOr;
// using InternetBank.Domain.Common.Errors;
// using InternetBank.Domain.Interfaces.UOF;
// using InternetBank.Domain.Repositories;
// using MediatR;

// namespace InternetBank.Application.Accounts.Commands.BlockAccount;

// public class BlockAccountCommandHandler : IRequestHandler<BlockAccountCommand, ErrorOr<bool>>
// {
//     private readonly IAccountRepository _accountRepository;
//     private readonly IUnitOfWork _unitOfWork;

//     public BlockAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
//     {
//         _accountRepository = accountRepository;
//         _unitOfWork = unitOfWork;
//     }

//     public async Task<ErrorOr<bool>> Handle(BlockAccountCommand request, CancellationToken cancellationToken)
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
