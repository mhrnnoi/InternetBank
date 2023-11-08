namespace InternetBank.Domain.Exceptions.Account;
using InternetBank.Domain.Abstracts.Exceptions;

public sealed class ExpiredAccount : DomainExceptions
{
    public const string Massage = "Account Is Expired";
    public const int StatusCodeConst = 400;
    public ExpiredAccount()
        : base(StatusCodeConst, Massage)
    {


    }
    public ExpiredAccount(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

