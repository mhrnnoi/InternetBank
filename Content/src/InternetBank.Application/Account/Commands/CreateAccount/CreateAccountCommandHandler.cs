using InternetBank.Domain.Accounts;
using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Account.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResult>
{
    private readonly IAccountRepository _accountRepository;

    public CreateAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<CreateAccountResult> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var acc = Domain.Accounts.Account.OpenAccount(request.AccountTypes, request.Amount, request.UserId);
        _accountRepository.AddAccount(acc);
        await Task.Yield();

        return new CreateAccountResult(acc.Number, acc.CardNumber, acc.CVV2, acc.ExpiryDate, acc.Password, acc.Id, acc.Type);


    }
}
