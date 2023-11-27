using ErrorOr;
using MediatR;

namespace InternetBank.Application.Transactions.Queries.GetReportQuery;

public record GetReportQuery(string SourceCardNumber,
                            DateOnly? From,
                            DateOnly? To,
                            bool? IsSuccess,
                            string UserId) : IRequest<ErrorOr<List<TransactionDTO>>>;


