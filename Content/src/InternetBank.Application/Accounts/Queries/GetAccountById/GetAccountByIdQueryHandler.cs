using ErrorOr;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Repositories;
using MapsterMapper;
using MediatR;

namespace InternetBank.Application.Accounts.Queries.GetAccountById;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, ErrorOr<AccountDTO>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public GetAccountByIdQueryHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<AccountDTO>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var acc = await _accountRepository.GetById(request.AccountId);
        if (acc is null )
            return Errors.Account.NotFoundAccount;
        if (acc.UserId!= request.UserId)
            return Errors.Account.AccountIsNotYours;

        var accDTO = _mapper.Map<AccountDTO>(acc);
        return accDTO ;



    }
}
