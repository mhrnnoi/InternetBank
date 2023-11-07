namespace InternetBank.Domain.Exceptions.Account;

public class IncorrectPass : DomainExceptions
{
    public const string Massage = "Incorrect Password";
    public const int StatusCodeConst = 400;

    public IncorrectPass()
: base(StatusCodeConst, Massage)
    {


    }
    public IncorrectPass(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

