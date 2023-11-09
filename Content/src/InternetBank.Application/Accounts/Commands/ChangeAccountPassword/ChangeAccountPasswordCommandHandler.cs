using ErrorOr;
using InternetBank.Domain.Accounts.Entities;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using InternetBank.Domain.ValueObjects;
using MediatR;

namespace InternetBank.Application.Accounts.Commands.ChangeAccountPassword;

public class ChangeAccountPasswordCommandHandler : IRequestHandler<ChangeAccountPasswordCommand, ErrorOr<bool>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeAccountPasswordCommandHandler(IAccountRepository accountRepository,
                                               IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<bool>> Handle(ChangeAccountPasswordCommand request, CancellationToken cancellationToken)
    {
        var acc = await _accountRepository.GetById(request.AccountId,
                                                   request.UserId);

        var oldPass = Password.Create(request.OldPassword);
        var newPass = Password.Create(request.NewPassword);
        var repeatNewPassword = RepeatPassword.Create(oldPass, newPass);
        var isPasswordChanged = Account.ChangePassword(oldPass,
                                                       newPass,
                                                       repeatNewPassword,
                                                       acc);

        if (isPasswordChanged.IsError)
            return isPasswordChanged.Errors;

        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
