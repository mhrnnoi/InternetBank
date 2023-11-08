using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using InternetBank.Domain.ValueObjects;
using MediatR;

namespace InternetBank.Application.Accounts.Commands.ChangeAccountPassword;

public class ChangeAccountPasswordCommandHandler : IRequestHandler<ChangeAccountPasswordCommand, bool>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeAccountPasswordCommandHandler(IAccountRepository accountRepository,
                                               IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ChangeAccountPasswordCommand request, CancellationToken cancellationToken)
    {
        var acc = await _accountRepository.GetById(request.AccountId,
                                                   request.UserId) ?? throw new Exception();
        var oldPass = Password.Create(request.OldPassword)                                                   ;
        var newPass = Password.Create(request.NewPassword)                                                   ;
        var repeatNewPassword = RepeatPassword.Create(oldPass, newPass);
        acc.ChangePassword(oldPass,
                           newPass,
                           repeatNewPassword);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
