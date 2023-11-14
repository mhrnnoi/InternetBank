namespace InternetBank.Domain.Exceptions.Transaction;
using InternetBank.Domain.Abstracts.Exceptions;

public sealed class IncorrectExpiryDate : DomainExceptions
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

