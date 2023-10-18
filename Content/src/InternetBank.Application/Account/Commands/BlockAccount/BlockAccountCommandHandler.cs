using MediatR;

namespace InternetBank.Application.Account.Commands.BlockAccount;

public class BlockAccountCommandHandler : IRequestHandler<BlockAccountCommand, bool>
{
    public Task<bool> Handle(BlockAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
