namespace InternetBank.Domain.Exceptions.Transaction;

public class IncorrectExpiryDate : DomainExceptions
{
    public const string Massage = "month or year of expiry is incorrect";
    public const int StatusCodeConst = 400;
    public const string TitleConst = "wrong expiry date";
    public IncorrectExpiryDate()
: base(StatusCodeConst, Massage)
    {


    }
    public IncorrectExpiryDate(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

