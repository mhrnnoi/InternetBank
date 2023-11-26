namespace InternetBank.Domain.Accounts.ValueObjects;

using InternetBank.Domain.Abstracts.Primitives;

public class Otp : ValueObject
{
    public string Value { get; set; }

    private Otp(string otp)
    {
        Value = otp;
        
    }

    public static Otp GenerateOTP()
    {
        string otp = "";
        var rand = new Random();
        for (int i = 0; i < 5; i++)
        {
            otp += rand.Next(0, 9);
        }
        return new Otp(otp);
    }
    public static Otp ConvertToOTP(string otp)
    {
        return new Otp(otp);
    }

    public override IEnumerable<object> GetAtomicValue()
    {
        yield return Value;
    }

    public static Otp Create(string value)
    {
        return new Otp(value);
    }
}