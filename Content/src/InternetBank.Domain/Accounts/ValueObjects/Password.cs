using ErrorOr;
using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Common.Errors;

namespace InternetBank.Domain.ValueObjects;

public class Password : ValueObject
{
    public string Value { get; set; }
    private Password(string password)
    {
        Value = password;
    }

    public override IEnumerable<object> GetAtomicValue()
    {
        yield return Value;
    }
    public static Password GeneratePassword()
    {
        var str = "";
        var rnd = new Random();
        for (int i = 0; i < 6; i++)
        {
            str += rnd.Next(0, 10);

        }
        return new Password(str);

    }
    public static ErrorOr<Password> GeneratePassword(string password)
    {
        if (password.Length is not 6 || !password.All(char.IsDigit))
            return Errors.Account.IncorrectPassFormat;
        return new Password(password);

    }

    public static Password Create(string value)
    {
        return new Password(value);
    }
}
