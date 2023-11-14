namespace InternetBank.Contracts.Requests.Users;

public record RegisterRequest(string FirstName,
                              string LastName,
                              string NationalCode,
                              DateOnly BirthDate,
                              string PhoneNumber,
                              string Email,
                              string Username,
                              string Password);