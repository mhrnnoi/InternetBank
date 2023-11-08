using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
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
        acc.ChangePassword(request.OldPassword,
                           request.NewPassword,
                           request.RepeatNewPassword);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
