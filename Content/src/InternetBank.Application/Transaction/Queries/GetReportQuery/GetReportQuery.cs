using MediatR;

namespace InternetBank.Application.Transaction.Queries.GetReportQuery;

public record GetReportQuery(DateOnly? From, DateOnly? To, bool? IsSuccess) : IRequest<List<TransactionDTO>>;

public record TransactionDTO(string Description,
                                double Amount,
                                DateTime CreatedDateTime,
                               string DestinationCardNumber);
