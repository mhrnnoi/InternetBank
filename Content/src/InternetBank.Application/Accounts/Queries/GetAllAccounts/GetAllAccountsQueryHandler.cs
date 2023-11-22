using InternetBank.Application.Accounts.Queries.GetAccountById;
using InternetBank.Domain.Repositories;
using MapsterMapper;
using MediatR;

namespace InternetBank.Application.Accounts.Queries.GetAllAccounts;

public class GetUserAllAccountsQueryHandler : IRequestHandler<GetUserAllAccountsQuery, List<AccountDTO>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public GetUserAllAccountsQueryHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<List<AccountDTO>> Handle(GetUserAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _accountRepository.GetUserAllAccounts(request.UserId);
        var accDTOs = _mapper.Map<List<AccountDTO>>(accounts);
        return accDTOs;
    }
}
