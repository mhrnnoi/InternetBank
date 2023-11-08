namespace InternetBank.Application.Transactions.Queries.GetReportQuery;
public record TransactionDTO(string Description,
                             double Amount,
                             DateTime CreatedDateTime,
                             string DestinationCardNumber,
                             bool IsSuccess,
                             string AccountId);