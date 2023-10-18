namespace InternetBank.Api.Requests.Users;

public record LoginRequest(string Email,
                           string Password);