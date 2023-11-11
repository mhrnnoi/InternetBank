using InternetBank.Domain.Abstracts.Primitives;

namespace InternetBank.Domain.Interfaces;

public interface IHasDomainEvents
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}