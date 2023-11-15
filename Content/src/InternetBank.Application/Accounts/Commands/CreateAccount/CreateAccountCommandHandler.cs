using InternetBank.Domain.Interfaces.UOF;
using InternetBank.Domain.Repositories;
using MapsterMapper;
using MediatR;
using InternetBank.Domain.Accounts.Entities;
using ErrorOr;
using InternetBank.Domain.Accounts.Enums;
using InternetBank.Application.Interfaces;

namespace InternetBank.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ErrorOr<CreateAccountResult>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public CreateAccountCommandHandler(IAccountRepository accountRepository,
                                       IUnitOfWork unitOfWork,
                                       IMapper mapper,
                                       IIdentityService identityService)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<ErrorOr<CreateAccountResult>> Handle(CreateAccountCommand request,
                                                  CancellationToken cancellationToken)
    {
        var accOrErrors = Account.OpenAccount(request.AccountType,
                                              request.Amount,
                                              request.UserId);
        if (accOrErrors.IsError)
            return accOrErrors.Errors;

        _accountRepository.AddAccount(accOrErrors.Value);
        
        await _unitOfWork.SaveChangesAsync();
        //Config needed for mapping
        return _mapper.Map<CreateAccountResult>(accOrErrors.Value);
    }
}
