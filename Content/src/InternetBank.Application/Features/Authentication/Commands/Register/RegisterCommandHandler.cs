using MediatR;

namespace InternetBank.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
{
    public Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        
    }
}
