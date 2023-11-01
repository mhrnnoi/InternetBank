using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Account.Commands.ChangeAccountPassword;

public class ChangeAccountPasswordCommandHandler : IRequestHandler<ChangeAccountPasswordCommand, bool>
{
    private readonly IAccountRepository _accountRepository;

    public ChangeAccountPasswordCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<bool> Handle(ChangeAccountPasswordCommand request, CancellationToken cancellationToken)
    {
        await _accountRepository.ChangePassword(request.AccountId, request.OldPassword, request.NewPassword, request.RepeatNewPassword);
        return true;
    }
}
