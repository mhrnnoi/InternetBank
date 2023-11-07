namespace InternetBank.Domain.Exceptions.Account;

public class IncorrectPassFormat : DomainExceptions
{
    public const string Massage = "Incorrect Password format : password should have 6 numeric characters";
    public const int StatusCodeConst = 400;

    public IncorrectPassFormat()
: base(StatusCodeConst, Massage)
    {


    }
    public IncorrectPassFormat(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

