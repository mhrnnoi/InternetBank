namespace InternetBank.Domain.Exceptions.Account;

public sealed class InvalidAccountType : DomainExceptions
{
    public const string Massage = "Incorrect Account type plz enter 1 for saving account or 2 for checking";
    public const int StatusCodeConst = 400;

    public InvalidAccountType()
: base(StatusCodeConst, Massage)
    {


    }
    public InvalidAccountType(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

