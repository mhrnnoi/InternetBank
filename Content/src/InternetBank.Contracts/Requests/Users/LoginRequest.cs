namespace InternetBank.Contracts.Requests.Users;

public record LoginRequest(string Email,
                              string Password);