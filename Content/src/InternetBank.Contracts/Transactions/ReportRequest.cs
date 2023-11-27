namespace InternetBank.Contracts.Transactions;

public record ReportRequest(string SourceCardNumber,
                            DateOnly From,
                            DateOnly To,
                            bool IsSuccess);