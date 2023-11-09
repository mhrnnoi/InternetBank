using InternetBank.Domain.Abstracts.Primitives;

namespace InternetBank.Domain.DomainEvents;

public sealed record AccountCreatedDomainEvent(string Id) : IDomainEvent;