using InternetBank.Domain.Abstracts.Primitives;

namespace InternetBank.Domain.Accounts.ValueObjects;

public class CardNumberVO : ValueObject
{
    public string CardNumber { get; set; }
    private CardNumberVO(string cardNumber)
    {
        CardNumber = cardNumber;
    }

    public static CardNumberVO GenerateCartNumber()
    {
        var strArr = new string[4];
        var str = "";
        var rnd = new Random();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                str += rnd.Next(0, 10);
            }
            strArr[i] = str;
            str = "";
        }
        return new CardNumberVO(string.Join("", strArr));
    }

    public override IEnumerable<object> GetAtomicValue()
    {
        yield return CardNumber;
    }
}