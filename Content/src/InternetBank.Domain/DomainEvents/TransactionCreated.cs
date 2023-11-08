using InternetBank.Domain.Abstracts.Primitives;

namespace InternetBank.Domain.DomainEvents;

public sealed record AccountCreated(string Id) : IDomainEvent;