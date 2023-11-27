namespace InternetBank.Domain.Accounts.ValueObjects;

using InternetBank.Domain.Abstracts.Primitives;

public class OtpExpireDate : ValueObject
{
    public DateTime Value { get; private set; }
    private OtpExpireDate(DateTime expiry)
    {
        Value = expiry;
    }
    public static OtpExpireDate GenerateOTPExpiryDate()
    {
        return new OtpExpireDate(DateTime.UtcNow.AddMinutes(2));
    }

    public override IEnumerable<object> GetAtomicValue()
    {
        yield return Value;
    }

    public static OtpExpireDate Create(DateTime dateTime)
    {
        return new OtpExpireDate(dateTime);

    }
}