using MediatR;

namespace InternetBank.Application.Account.Commands.ChangeAccountPassword;

public record ChangeAccountPasswordCommand(int AccountId,
                                           string UserId,
                                           string OldPassword,
                                           string NewPassword,
                                           string RepeatNewPassword) :IRequest<bool>;

