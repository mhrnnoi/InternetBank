// using ErrorOr;
// using InternetBank.Domain.Accounts.Entities;
// using InternetBank.Domain.Common.Errors;
// using InternetBank.Domain.Interfaces.UOF;
// using InternetBank.Domain.Repositories;
// using InternetBank.Domain.ValueObjects;
// using MediatR;

// namespace InternetBank.Application.Accounts.Commands.ChangeAccountPassword;

// public class ChangeAccountPasswordCommandHandler : IRequestHandler<ChangeAccountPasswordCommand, ErrorOr<bool>>
// {
//     private readonly IAccountRepository _accountRepository;
//     private readonly IUnitOfWork _unitOfWork;

//     public ChangeAccountPasswordCommandHandler(IAccountRepository accountRepository,
//                                                IUnitOfWork unitOfWork)
//     {
//         _accountRepository = accountRepository;
//         _unitOfWork = unitOfWork;
//     }

//     public async Task<ErrorOr<bool>> Handle(ChangeAccountPasswordCommand request, CancellationToken cancellationToken)
//     {
//         var acc = await _accountRepository.GetById(request.AccountId);
//         if (acc is null)
//         {
//             return Errors.User.NotFoundAccountById;
//         }
//         if (acc.UserId != request.UserId)
//         {
//             return Errors.Account.AccountIsNotYours;
//         }

//         var oldPass = Password.GeneratePassword(request.OldPassword);

//         if (oldPass.IsError)
//             return Errors.Account.IncorrectPass;

//         var newPass = Password.GeneratePassword(request.NewPassword);
//         var repeatNewPassword = Password.GeneratePassword(request.RepeatNewPassword);
//         // if (newPass.IsError != true && repeatNewPassword.IsError != true)
//         // {
//         //     var isPasswordChanged = acc.ChangePassword(oldPass.Value,
//         //                                                newPass.Value,
//         //                                                repeatNewPassword.Value);

//         //     if (isPasswordChanged.IsError)
//         //         return isPasswordChanged.Errors;

//         //     await _unitOfWork.SaveChangesAsync();
//         //     return true;
//         // }
//         return Errors.Account.IncorrectPassFormat;

//     }
// }
