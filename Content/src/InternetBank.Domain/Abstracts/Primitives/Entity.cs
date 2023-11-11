using InternetBank.Domain.Interfaces;

namespace InternetBank.Domain.Abstracts.Primitives;

public abstract class Entity : IEquatable<Entity>, IHasDomainEvents
{
    public string Id { get; private init; }
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    protected Entity()
    {
        Id = Guid.NewGuid().ToString();
    }
    public static bool operator ==(Entity? e1, Entity? e2)
    {
        if (e1 is not null)
        {
            if (e2 is not null)
                return e1.Equals(e2);
            return false;
        }
        return false;
    }
    public static bool operator !=(Entity? e1, Entity? e2) => !(e1 == e2);



    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;
        if (obj is Entity en)
            return en.Id == Id;
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

    public bool Equals(Entity? other)
    {
        if (other is null || other.GetType() != GetType())
            return false;
        return other.Id == Id;
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

}