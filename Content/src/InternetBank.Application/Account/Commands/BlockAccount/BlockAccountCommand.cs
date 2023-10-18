using MediatR;

namespace InternetBank.Application.Account.Commands.BlockAccount;

public record BlockAccountCommand(int Id):IRequest<bool>;