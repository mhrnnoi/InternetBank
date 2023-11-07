namespace InternetBank.Contracts.Requests.Transactions;

public record TransferMoneyRequest(string OTP, double Amount);