using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Accounts.Entities;

namespace InternetBank.Domain.Accounts.Events;

public record AccountCreatedDomainEvent(Account Account) : IDomainEvent;
