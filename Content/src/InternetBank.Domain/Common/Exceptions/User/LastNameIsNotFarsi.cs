namespace InternetBank.Domain.Exceptions.User;
using InternetBank.Domain.Abstracts.Exceptions;

public sealed class LastNameIsNotFarsi : DomainExceptions
{
    public const string Massage = "Plz Write Last name with persian keyboard";
    public const int StatusCodeConst = 400;

    public LastNameIsNotFarsi()
: base(StatusCodeConst, Massage)
    {


    }
    public LastNameIsNotFarsi(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}
