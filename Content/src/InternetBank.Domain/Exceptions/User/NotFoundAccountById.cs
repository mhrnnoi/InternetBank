namespace InternetBank.Domain.Exceptions.User;
public sealed class NotFoundAccountById : DomainExceptions
{
    public const string Massage = "there is no account with this id";
    public const int StatusCodeConst = 404;

    public NotFoundAccountById()
: base(StatusCodeConst, Massage)
    {


    }
    public NotFoundAccountById(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}
