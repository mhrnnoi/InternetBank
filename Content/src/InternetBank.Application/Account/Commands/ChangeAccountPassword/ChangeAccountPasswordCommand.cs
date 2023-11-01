using MediatR;

namespace InternetBank.Application.Account.Commands.ChangeAccountPassword;

public record ChangeAccountPasswordCommand(int AccountId, string OldPassword, string NewPassword, string RepeatNewPassword):IRequest<bool>;

