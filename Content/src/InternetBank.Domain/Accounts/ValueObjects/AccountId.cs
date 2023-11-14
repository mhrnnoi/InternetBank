using InternetBank.Domain.Abstracts.Primitives;

namespace InternetBank.Domain.Accounts.ValueObjects;

public class AccountId : ValueObject
{
    public Guid Id { get;}
    private AccountId(Guid id)
    {
        Id = id;
    }

    public static AccountId GenerateId()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetAtomicValue()
    {
        yield return Id;
    }
}