using MediatR;

namespace InternetBank.Application.Features.Authentication.Commands.Register;

public record RegisterCommand(string FirstName,
                              string LastName,
                              string NationalCode,
                              DateTime BirthDate,
                              string PhoneNumber,
                              string Email) : IRequest<bool>;