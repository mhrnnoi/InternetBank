using System.IO.Compression;
using ErrorOr;
using InternetBank.Domain.Accounts.ValueObjects;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Repositories;
using MapsterMapper;
using MediatR;

namespace InternetBank.Application.Transactions.Queries.GetReportQuery;

public class GetReportQueryHandler : IRequestHandler<GetReportQuery, ErrorOr<List<TransactionDTO>>>
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

    public async Task<ErrorOr<List<TransactionDTO>>> Handle(GetReportQuery request, CancellationToken cancellationToken)
    {
        var cardNumber = CardNumber.Create(request.SourceCardNumber);
        var acc = await _accountRepository.GetByCardNumber(cardNumber);
        if (acc is null)
            return Errors.Account.NotFoundAccount;

        if (acc.UserId != request.UserId)
            return Errors.Account.AccountIsNotYours;

        var transactions = acc.GetTransactionsByDateAndSuccess(request.From, request.To, request.IsSuccess);
        var transactionDTO = _mapper.Map<List<TransactionDTO>>(transactions);

        return transactionDTO;
    }


}
