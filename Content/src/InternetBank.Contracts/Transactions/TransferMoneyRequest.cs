namespace InternetBank.Contracts.Requests.Transactions;

public record TransferMoneyRequest(string Otp, double Amount);