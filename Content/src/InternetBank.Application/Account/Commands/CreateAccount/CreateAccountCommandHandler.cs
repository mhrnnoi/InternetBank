using InternetBank.Domain.Accounts;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Account.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResult>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateAccountResult> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {

        var acc = _accountRepository.CreateAccount(request.AccountTypes, request.Amount, request.UserId);
        await _unitOfWork.SaveChangesAsync();

        return new CreateAccountResult(acc.Number,
                                       acc.CardNumber,
                                       acc.CVV2,
                                       acc.ExpiryDate,
                                       acc.Password,
                                       acc.Id,
                                       acc.Type);


    }
}
