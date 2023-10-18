using MediatR;

namespace InternetBank.Application.Features.Account.Commands.BlockAccount;

public class BlockAccountCommandHandler : IRequestHandler<BlockAccountCommand, bool>
{
    public Task<bool> Handle(BlockAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
