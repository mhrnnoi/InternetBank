namespace InternetBank.Contracts.Requests.Transactions;
public record OTPRequest(string CardNumber,
                         string Cvv2,
                         string ExpiryYear,
                         string ExpiryMonth,
                         double Amount,
                         string DestinationCardNumber);