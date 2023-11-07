namespace InternetBank.Domain.Exceptions.Account;

public class AccountIsBlocked : DomainExceptions
{
    public const string Massage = "Account is blocked so no transfering can be done both recieve and send";
    public const int StatusCodeConst = 400;
    public AccountIsBlocked()
        : base(StatusCodeConst, Massage)
    {


    }
    public AccountIsBlocked(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

