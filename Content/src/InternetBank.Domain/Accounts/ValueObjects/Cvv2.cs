// using InternetBank.Domain.Abstracts.Primitives;

// namespace InternetBank.Domain.Accounts.ValueObjects;

// public class Cvv2 : ValueObject
// {
//     public string Value { get; set; }
//     private Cvv2(string cvv2)
//     {
//         Value = cvv2;
//     }

//     public static Cvv2 GenerateCVV2()
//     {
//         var str = "";
//         var rnd = new Random();
//         for (int i = 0; i < 4; i++)
//         {
//             str += rnd.Next(0, 10);
//         }
//         return new Cvv2(str);
//     }

//     public override IEnumerable<object> GetAtomicValue()
//     {
//         yield return Value;
//     }
// }