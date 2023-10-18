using MediatR;

namespace InternetBank.Application.Account.Commands.UnBlockAccount;

public class UnBlockAccountCommandHandler : IRequestHandler<UnBlockAccountCommand, bool>
{
    public Task<bool> Handle(UnBlockAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
