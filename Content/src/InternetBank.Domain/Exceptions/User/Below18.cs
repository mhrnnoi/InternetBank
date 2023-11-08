namespace InternetBank.Domain.Exceptions.User;
public sealed class Below18 : DomainExceptions
{
    public const string Massage = "you're not in supported age range ";
    public const int StatusCodeConst = 400;

    public Below18()
: base(StatusCodeConst, Massage)
    {


    }
    public Below18(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

