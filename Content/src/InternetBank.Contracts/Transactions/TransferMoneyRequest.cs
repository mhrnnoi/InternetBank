namespace InternetBank.Contracts.Requests.Transactions;

public record TransferMoneyRequest(double Amount,
                                   string Cvv2,
                                   string ExpiryYear,
                                   string ExpiryMonth,
                                   string Otp,
                                   string DestinationCardNumber,
                                   string SourceCardNumber);