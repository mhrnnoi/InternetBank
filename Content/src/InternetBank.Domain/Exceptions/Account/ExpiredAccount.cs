namespace InternetBank.Domain.Exceptions.Account;
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

