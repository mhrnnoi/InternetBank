using InternetBank.Domain.Abstracts.Primitives;

namespace InternetBank.Domain.Accounts.ValueObjects;

public class CardNumber : ValueObject
{
    public string Value { get; set; }
    private CardNumber(string cardNumber)
    {
        Value = cardNumber;
    }

    public static CardNumber GenerateCartNumber()
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
        return new CardNumber(string.Join("", strArr));
    }

    public override IEnumerable<object> GetAtomicValue()
    {
        yield return Value;
    }

    public static CardNumber Create(string value)
    {
        return new CardNumber(value);
    }
}