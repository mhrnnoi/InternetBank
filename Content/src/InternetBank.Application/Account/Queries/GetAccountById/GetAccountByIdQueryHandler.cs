using InternetBank.Application.Account.Queries.GetAccountById;
using InternetBank.Domain.Exceptions.User;
using InternetBank.Domain.Repositories;
using MapsterMapper;
using MediatR;

namespace InternetBank.Application.Account.Queries.GetById;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDTO>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public GetAccountByIdQueryHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<AccountDTO> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var acc = await _accountRepository.GetById(request.Id, request.UserId) ?? throw new NotFoundAccountById();
        var accDTO = _mapper.Map<AccountDTO>(acc);
        return accDTO ;



    }
}
