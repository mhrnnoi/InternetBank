using InternetBank.Domain.Abstracts.Primitives;

namespace InternetBank.Domain.Accounts.Events;

public record AccountCreatedDomainEvent(string AccountType,
                                        double Amount,
                                        string AccountNumber,
                                        string CardNumber,
                                        string UserId) : IDomainEvent;
