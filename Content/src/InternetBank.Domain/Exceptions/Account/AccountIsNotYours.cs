namespace InternetBank.Domain.Exceptions.Account;
using InternetBank.Domain.Abstracts.Exceptions;

public sealed class AccountIsNotYours : DomainExceptions
{
    public const string Massage = "Account Is Not yours";
    public const int StatusCodeConst = 400;
    public AccountIsNotYours()
        : base(StatusCodeConst, Massage)
    {


    }
    public AccountIsNotYours(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}
