using InternetBank.Domain.Interfaces;

namespace InternetBank.Domain.Abstracts.Primitives;

public abstract class Entity<TID> : IEquatable<Entity<TID>>, IHasDomainEvents where TID : notnull
{
    
    public TID Id { get; private init; }
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    protected Entity(TID id)
    {
        Id = id;
    }
    public static bool operator ==(Entity<TID>? e1, Entity<TID>? e2)
    {
        if (e1 is not null)
        {
            if (e2 is not null)
                return e1.Equals(e2);
            return false;
        }
        return false;
    }
    public static bool operator !=(Entity<TID>? e1, Entity<TID>? e2) => !(e1 == e2);



    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;
        if (obj is Entity<TID> en)
            return en.Id.Equals(Id);
        return false;
    }
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }


    public override int GetHashCode()
    {
        return Id.GetHashCode() * 66;
    }

    public bool Equals(Entity<TID>? other)
    {
        if (other is null || other.GetType() != GetType())
            return false;
        return other.Id.Equals(Id);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }


}