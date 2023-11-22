using InternetBank.Domain.Abstracts.Primitives;

namespace InternetBank.Domain.Accounts.ValueObjects;

public class AccountId : ValueObject
{
    public Guid Value { get;}
    private AccountId(Guid id)
    {
        Value = id;
    }

    public static AccountId GenerateId()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetAtomicValue()
    {
        yield return Value;
    }

    public static AccountId Create(Guid value)
    {
        return new AccountId(value);
    }

    public static AccountId? Parse(string id)
    {
        try
        {
            return new AccountId(Guid.Parse(id));
        }
        catch (Exception)
        {
            return null;
        }
    }
}