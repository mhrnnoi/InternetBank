using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Exceptions.Account;

namespace InternetBank.Domain.ValueObjects;

public class RepeatPassword : ValueObject
{
    public string Value { get; set; }
    private RepeatPassword(Password value)
    {
        Value = value.Value;
    }

    public override IEnumerable<object> GetAtomicValue()
    {
        yield return Value;
    }
    public static RepeatPassword Create(Password oldPass, Password newPassword)
    {
        if (oldPass.Value != newPassword.Value)
            throw new PassAndRepeatPassIsNotSame();

        if (newPassword.Value.Length is not 6 || !newPassword.Value.All(char.IsDigit))
            throw new IncorrectPassFormat();

        return new RepeatPassword(newPassword);
    }
}
