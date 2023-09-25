namespace InternetBank.Domain.Abstracts.Entity;

public abstract class Entity
{
    public Guid Id { get; init; }

    public static bool operator ==(Entity e1, Entity e2) => e1.Id == e2.Id ;
    public static bool operator !=(Entity e1, Entity e2) => e1.Id != e2.Id ;
    
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not null && obj is Entity entity)
        {
            return Id == entity.Id;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() + 66;
    }
}