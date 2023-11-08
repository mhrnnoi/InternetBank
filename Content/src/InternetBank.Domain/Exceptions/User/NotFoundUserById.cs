namespace InternetBank.Domain.Exceptions.User;
using InternetBank.Domain.Abstracts.Exceptions;

public sealed class NotFoundUserById : DomainExceptions
{
    public const string Massage = "there is no user with this id";
    public const int StatusCodeConst = 404;

    public NotFoundUserById()
: base(StatusCodeConst, Massage)
    {


    }
    public NotFoundUserById(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

