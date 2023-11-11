using MediatR;

namespace InternetBank.Application.Transactions.Queries.GetReportQuery;

public record GetReportQuery(DateOnly From,
                             DateOnly To,
                             bool IsSuccess) : IRequest<List<TransactionDTO>>;


