using InternetBank.Domain.Exceptions.User;
using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Accounts.Commands.BlockAccount;

public class BlockAccountCommandHandler : IRequestHandler<BlockAccountCommand, bool>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BlockAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(BlockAccountCommand request, CancellationToken cancellationToken)
    {
        var acc = await _accountRepository.GetById(request.Id,
                                                   request.UserId) ?? throw new NotFoundAccountById();
        acc.BlockAccount();
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
