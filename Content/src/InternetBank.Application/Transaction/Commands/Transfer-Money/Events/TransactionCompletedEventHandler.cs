using InternetBank.Application.Interfaces;
using InternetBank.Domain.Accounts.Events;
using InternetBank.Domain.Repositories;
using MediatR;

namespace InternetBank.Application.Transaction.Commands.Transfer_Money.Events;

public class TransactionCompletedEventHandler : INotificationHandler<TransactionCompletedDomainEvent>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IIdentityService _identityService;
    public TransactionCompletedEventHandler(ITransactionRepository transactionRepository, IIdentityService identityService)
    {
        _transactionRepository = transactionRepository;
        _identityService = identityService;
    }
    public async Task Handle(TransactionCompletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetByIdAsync(notification.UserId);
        if (user is null)
            await Task.Yield();
        else
        {
            _transactionRepository.SendEmailForCompleteTransaction(user.Email,
                                                                   notification.Amount,
                                                                   notification.DestinationCardNumber,
                                                                   notification.SourceCardNumber,
                                                                   notification.UserId);
            await Task.Yield();
        }
    }
}
