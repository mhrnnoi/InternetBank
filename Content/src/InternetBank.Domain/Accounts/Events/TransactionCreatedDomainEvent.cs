using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Accounts.Entities;

namespace InternetBank.Domain.Accounts.Events;

public record TransactionCreatedDomainEvent(double Amount,
                                            string DestinationCardNumber,
                                            string SourceCardNumber,
                                            string UserId) : IDomainEvent;
