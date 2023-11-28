using System.Data.Common;
using InternetBank.Application.Interfaces;
using InternetBank.Domain.Accounts.Events;
using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Accounts.Commands.CreateAccount.Events;

public class AccountCreatedEventHandler : INotificationHandler<AccountCreatedDomainEvent>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IIdentityService _identityService;
    public AccountCreatedEventHandler(ITransactionRepository transactionRepository, IIdentityService identityService)
    {
        _transactionRepository = transactionRepository;
        _identityService = identityService;
    }
    public async Task Handle(AccountCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetByIdAsync(notification.UserId);
        if (user is null)
            await Task.Yield();
        else
        {
            _transactionRepository.SendEmailForCreatingAccount(user.Email,
                                                               notification.AccountType,
                                                               notification.Amount,
                                                               notification.AccountNumber,
                                                               notification.CardNumber);
            await Task.Yield();
        }


    }
}
