using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MediatR;
using static InternetBank.Domain.Exceptions.DomainExceptions.User;

namespace InternetBank.Application.Account.Commands.UnBlockAccount;

public class UnBlockAccountCommandHandler : IRequestHandler<UnBlockAccountCommand, bool>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnBlockAccountCommandHandler(IUnitOfWork unitOfWork, IAccountRepository accountRepository)
    {
        _unitOfWork = unitOfWork;
        _accountRepository = accountRepository;
    }

    public async Task<bool> Handle(UnBlockAccountCommand request, CancellationToken cancellationToken)
    {
        var acc = await _accountRepository.GetById(request.Id, request.UserId) ?? throw new NotFoundAccountById();
        acc.UnBlockAccount();
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
