// using InternetBank.Domain.Abstracts.Primitives;
// using InternetBank.Domain.Accounts.Enums;

// namespace InternetBank.Domain.Accounts.ValueObjects;

// public class AccountNumber : ValueObject
// {
//     public string Value { get; set; }
//     private AccountNumber(string accountNumber)
//     {
//         Value = accountNumber;
//     }

//     public static AccountNumber GenerateAccountNumber(string userId, AccountTypes accountType)
//     {
//         var strArr = new string[3];
//         var str = "";
//         var rnd = new Random();
//         for (int i = 0; i < 2; i++)
//         {
//             str += rnd.Next(0, 10);
//         }
//         strArr[0] = str;
//         str = "";
//         var strUserId = userId.ToString();
//         for (int i = 0; i < 3; i++)
//         {
//             str += strUserId[i];
//         }

//         for (int i = 0; i < 4; i++)
//         {
//             str += rnd.Next(0, 10);
//         }

//         strArr[1] = str;
//         str = "";

//         str += (int)accountType;



//         strArr[2] = str;
//         return new AccountNumber(string.Join(".", strArr));
//     }

//     public override IEnumerable<object> GetAtomicValue()
//     {
//         yield return Value;
//     }
// }