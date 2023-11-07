using MediatR;

namespace InternetBank.Application.Transaction.Queries.GetReportQuery;

public record GetReportQuery(DateOnly? From,
                             DateOnly? To,
                             bool? IsSuccess) : IRequest<List<TransactionDTO>>;


