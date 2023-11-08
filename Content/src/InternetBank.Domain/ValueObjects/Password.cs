using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Exceptions.Account;

namespace InternetBank.Domain.ValueObjects;

public class Password : ValueObject
{
    public string Value { get; set; }
    private Password(string value)
    {
        Value = value;
    }

    public override IEnumerable<object> GetAtomicValue()
    {
        yield return Value;
    }
    public static Password Create(string value)
    {
        if (value.Length is 6 && value.All(char.IsDigit))
            return new Password(value);

        throw new IncorrectPassFormat(); 
    }
}
