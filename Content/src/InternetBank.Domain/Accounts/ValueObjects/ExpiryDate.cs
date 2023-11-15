// using InternetBank.Domain.Abstracts.Primitives;

// namespace InternetBank.Domain.Accounts.ValueObjects;

// public class ExpiryDate : ValueObject
// {
//     public string ExpiryYear { get; init; }
//     public string ExpiryMonth { get; init; }
//     private ExpiryDate(DateTime dateTime)
//     {
//         ExpiryYear = dateTime.Year.ToString();
//         ExpiryMonth = dateTime.Month.ToString();;
//     }

//     public static ExpiryDate SetExpiry()
//     {
//         var expiry = DateTime.UtcNow.AddYears(5);
//         return new ExpiryDate(expiry);
//     }


//     public override IEnumerable<object> GetAtomicValue()
//     {
//         yield return ExpiryYear;
//         yield return ExpiryMonth;
//     }
// }