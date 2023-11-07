using InternetBank.Domain.Repositories;
using MapsterMapper;
using MediatR;

namespace InternetBank.Application.Transaction.Queries.GetReportQuery;

public class GetReportQueryHandler : IRequestHandler<GetReportQuery, List<TransactionDTO>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public GetReportQueryHandler(IAccountRepository accountRepository, ITransactionRepository transactionRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<List<TransactionDTO>> Handle(GetReportQuery request, CancellationToken cancellationToken)
    {
        var transactions =  await _transactionRepository.GetByDateAndSuccess(request.From,
                                                                             request.To,
                                                                             request.IsSuccess);
        var transactionDTO = _mapper.Map<List<TransactionDTO>>(transactions);
        return transactionDTO;
    }
}
