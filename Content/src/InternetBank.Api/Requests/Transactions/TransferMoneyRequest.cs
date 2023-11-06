namespace InternetBank.Api.Requests.Transactions;

public record TransferMoneyRequest(string OTP, double Amount);