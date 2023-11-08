namespace InternetBank.Domain.Exceptions.User;
using InternetBank.Domain.Abstracts.Exceptions;

public sealed class InvalidCred : DomainExceptions
{
    public const string Massage = "Incorrect email or Password";
    public const int StatusCodeConst = 401;

    public InvalidCred()
: base(StatusCodeConst, Massage)
    {


    }
    public InvalidCred(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

