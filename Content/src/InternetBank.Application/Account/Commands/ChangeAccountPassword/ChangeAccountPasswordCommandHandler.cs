using MediatR;

namespace InternetBank.Application.Account.Commands.ChangeAccountPassword;

public class ChangeAccountPasswordCommandHandler : IRequestHandler<ChangeAccountPasswordCommand, string>
{
    public Task<string> Handle(ChangeAccountPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
