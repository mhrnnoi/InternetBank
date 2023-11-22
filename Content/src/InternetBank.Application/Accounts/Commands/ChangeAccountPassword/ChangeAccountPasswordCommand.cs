using ErrorOr;
using MediatR;

namespace InternetBank.Application.Accounts.Commands.ChangeAccountPassword;

public record ChangeAccountPasswordCommand(string AccountId,
                                           string UserId,
                                           string OldPassword,
                                           string NewPassword,
                                           string RepeatNewPassword) : IRequest<ErrorOr<bool>>;

