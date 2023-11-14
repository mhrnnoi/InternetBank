using ErrorOr;
using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Exceptions.Account;

namespace InternetBank.Domain.ValueObjects;

public class PasswordVO : ValueObject
{
    public string Password { get; set; }
    private PasswordVO(string password)
    {
        Password = password;
    }

    public override IEnumerable<object> GetAtomicValue()
    {
        yield return Password;
    }
    public static PasswordVO GeneratePassword()
    {
        var str = "";
        var rnd = new Random();
        for (int i = 0; i < 6; i++)
        {
            str += rnd.Next(0, 10);

        }
        return new PasswordVO(str);

    }
    public static ErrorOr<PasswordVO> GeneratePassword(string password)
    {
        if (password.Length is not 6 || !password.All(char.IsDigit))
            return Errors.Account.IncorrectPassFormat;
        return new PasswordVO(password);

    }
}
