namespace InternetBank.Domain.Exceptions.Transaction;

public sealed class NotYourTransaction : DomainExceptions
{
    public const string Massage = "you dont have transaction with this information";
    public const int StatusCodeConst = 400;
    public const string TitleConst = "wrong transaction";
    public NotYourTransaction()
: base(StatusCodeConst, Massage)
    {


    }
    public NotYourTransaction(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}
