using InternetBank.Domain.Abstracts.Primitives;

namespace InternetBank.Domain.Accounts.ValueObjects;

public class ExpiryDateVO : ValueObject
{
    public string ExpiryYear { get; init; }
    public string ExpiryMonth { get; init; }
    private ExpiryDateVO(DateTime dateTime)
    {
        ExpiryYear = dateTime.Year.ToString();
        ExpiryMonth = dateTime.Month.ToString();;
    }

    public static ExpiryDateVO SetExpiry()
    {
        var expiry = DateTime.UtcNow.AddYears(5);
        return new ExpiryDateVO(expiry);
    }


    public override IEnumerable<object> GetAtomicValue()
    {
        yield return ExpiryYear;
        yield return ExpiryMonth;
    }
}