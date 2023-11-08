using InternetBank.Domain.Repositories;
using MapsterMapper;
using MediatR;

namespace InternetBank.Application.Accounts.Queries.GetAllAccounts;

public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AllAccountsDTO>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public GetAllAccountsQueryHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<List<AllAccountsDTO>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts =  await _accountRepository.GetAllAccounts(request.UserId);
        var accDTOs =  _mapper.Map<List<AllAccountsDTO>>(accounts);
        return accDTOs;
    }
}
