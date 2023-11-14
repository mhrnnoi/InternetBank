using InternetBank.Domain.Abstracts.Primitives;

namespace InternetBank.Domain.Accounts.ValueObjects;

public class AccountNumberVO : ValueObject
{
    public string AccountNumber { get; set; }
    private AccountNumberVO(string accountNumber)
    {
        AccountNumber = accountNumber;
    }

    public static AccountNumberVO GenerateAccountNumber(string userId, int accountType)
    {
        var strArr = new string[3];
        var str = "";
        var rnd = new Random();
        for (int i = 0; i < 2; i++)
        {
            str += rnd.Next(0, 10);
        }
        strArr[0] = str;
        str = "";
        var strUserId = userId.ToString();
        for (int i = 0; i < 3; i++)
        {
            str += strUserId[i];
        }

        for (int i = 0; i < 4; i++)
        {
            str += rnd.Next(0, 10);
        }

        strArr[1] = str;
        str = "";

        str += accountType;


        strArr[2] = str;
        return new AccountNumberVO(string.Join(".", strArr));
    }

    public override IEnumerable<object> GetAtomicValue()
    {
        yield return AccountNumber;
    }
}