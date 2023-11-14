using InternetBank.Domain.Abstracts.Primitives;

namespace InternetBank.Domain.Accounts.ValueObjects;

public class Cvv2VO : ValueObject
{
    public string Cvv2 { get; set; }
    private Cvv2VO(string cvv2)
    {
        Cvv2 = cvv2;
    }

    public static Cvv2VO GenerateCVV2()
    {
        var str = "";
        var rnd = new Random();
        for (int i = 0; i < 4; i++)
        {
            str += rnd.Next(0, 10);
        }
        return new Cvv2VO(str);
    }

    public override IEnumerable<object> GetAtomicValue()
    {
        yield return Cvv2;
    }
}