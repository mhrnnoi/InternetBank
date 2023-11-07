namespace InternetBank.Domain.Exceptions.User;
public class IncorrectNationalCode : DomainExceptions
{
    public const string Massage = "Plz Write Correct National Code";
    public const int StatusCodeConst = 400;

    public IncorrectNationalCode()
: base(StatusCodeConst, Massage)
    {


    }
    public IncorrectNationalCode(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}
