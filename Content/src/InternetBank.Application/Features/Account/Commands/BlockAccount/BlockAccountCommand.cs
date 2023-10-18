using MediatR;

namespace InternetBank.Application.Features.Account.Commands.BlockAccount;

public record BlockAccountCommand(int Id):IRequest<bool>;