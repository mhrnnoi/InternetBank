namespace InternetBank.Application.Transaction.Queries.GetReportQuery;
public record TransactionDTO(string Description,
                             double Amount,
                             DateTime CreatedDateTime,
                             string DestinationCardNumber,
                             bool IsSuccess,
                             int AccountId);