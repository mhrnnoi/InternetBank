namespace InternetBank.Domain.Exceptions.User;
public sealed class NotYourAccount : DomainExceptions
{
    public const string Massage = "you dont have any account with this id";
    public const int StatusCodeConst = 404;

    public NotYourAccount()
: base(StatusCodeConst, Massage)
    {


    }
    public NotYourAccount(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

