using MediatR;

namespace InternetBank.Application.Authentication.Commands.Register;

public record RegisterCommand(string FirstName,
                              string LastName,
                              string NationalCode,
                              DateOnly BirthDate,
                              string PhoneNumber,
                              string Email,
                              string Username,
                              string Password) : IRequest<RegisterActionResult>;