namespace InternetBank.Domain.Exceptions.User;
public sealed class FirstNameIsNotFarsi : DomainExceptions
{
    public const string Massage = "Plz Write first name with persian keyboard";
    public const int StatusCodeConst = 400;

    public FirstNameIsNotFarsi()
: base(StatusCodeConst, Massage)
    {


    }
    public FirstNameIsNotFarsi(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

