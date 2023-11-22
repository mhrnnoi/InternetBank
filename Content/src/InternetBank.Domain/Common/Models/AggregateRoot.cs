namespace InternetBank.Domain.Abstracts.Primitives;

public abstract class AggregateRoot<TID>: Entity<TID>
    where TID  : notnull
{
    protected AggregateRoot()
    {
        
    }
    protected AggregateRoot(TID id) : base(id)
    {
    }
}