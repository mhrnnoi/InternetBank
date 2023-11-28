using InternetBank.Application.Interfaces;
using InternetBank.Domain.Accounts.Events;
using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Transaction.Commands.Common.Events;

public class TransactionCreatedEventHandler : INotificationHandler<TransactionCreatedDomainEvent>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IIdentityService _identityService;
    public TransactionCreatedEventHandler(ITransactionRepository transactionRepository, IIdentityService identityService)
    {
        _transactionRepository = transactionRepository;
        _identityService = identityService;
    }
    public async Task Handle(TransactionCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetByIdAsync(notification.UserId);
        if (user is null)
            await Task.Yield();
        else
        {
            _transactionRepository.SendEmailForCreatingTransaction(user.Email,
                                                                   notification.Amount,
                                                                   notification.DestinationCardNumber,
                                                                   notification.SourceCardNumber,
                                                                   notification.UserId);
            await Task.Yield();
        }
    }
}
