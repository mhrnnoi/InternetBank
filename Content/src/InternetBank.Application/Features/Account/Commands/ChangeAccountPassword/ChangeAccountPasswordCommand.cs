using MediatR;

namespace InternetBank.Application.Features.Account.Commands.ChangeAccountPassword;

public record ChangeAccountPasswordCommand(ChangeAccountPasswordDTO ChangeAccountPasswordDTO):IRequest<string>;

public record ChangeAccountPasswordDTO(string OldPassword, string NewPassword, string RepeatNewPassword)
{
}