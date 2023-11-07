using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MapsterMapper;
using MediatR;

namespace InternetBank.Application.Account.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResult>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAccountCommandHandler(IAccountRepository accountRepository,
                                       IUnitOfWork unitOfWork,
                                       IMapper mapper)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateAccountResult> Handle(CreateAccountCommand request,
                                                  CancellationToken cancellationToken)
    {
        var acc = Domain.Accounts.Account.OpenAccount(request.AccountType,
                                                      request.Amount,
                                                      request.UserId);
        _accountRepository.AddAccount(acc);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CreateAccountResult>(acc);
    }
}
