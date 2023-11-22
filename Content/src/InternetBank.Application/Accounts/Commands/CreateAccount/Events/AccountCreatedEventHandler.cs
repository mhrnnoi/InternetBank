using InternetBank.Domain.Accounts.Events;
using MediatR;

namespace InternetBank.Application.Accounts.Commands.CreateAccount.Events;

public class AccountCreatedEventHandler : INotificationHandler<AccountCreatedDomainEvent>
{
    public async Task Handle(AccountCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        //send email that account created 
        await Task.Yield();
    }
}
