namespace InternetBank.Domain.Abstracts.Entity;

public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; private init; }

    protected Entity()
    {
        Id = Guid.NewGuid();
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
}