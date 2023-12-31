namespace InternetBank.Contracts.Requests.Users;

public record RegisterRequest(string FirstName,
                              string LastName,
                              string NationalCode,
                              DateTime BirthDate,
                              string PhoneNumber,
                              string Email);