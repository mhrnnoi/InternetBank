using InternetBank.Domain.Abstracts.Primitives;

namespace InternetBank.Domain.Accounts.ValueObjects;

public class TransactionId : ValueObject
{
    public Guid Value { get;}
    private TransactionId(Guid id)
    {
        Value = id;
    }

    public static TransactionId GenerateId()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetAtomicValue()
    {
        yield return Value;
    }
}