using InternetBank.Domain.Abstracts.Primitives;

namespace InternetBank.Domain.Accounts.ValueObjects;

public class TransactionId : ValueObject
{
    public Guid Id { get;}
    private TransactionId(Guid id)
    {
        Id = id;
    }

    public static TransactionId GenerateId()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetAtomicValue()
    {
        yield return Id;
    }
}