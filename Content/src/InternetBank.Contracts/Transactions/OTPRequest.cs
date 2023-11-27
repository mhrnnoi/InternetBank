namespace InternetBank.Contracts.Requests.Transactions;
public record OTPRequest(string CardNumber,
                         double Amount,
                         string DestinationCardNumber);