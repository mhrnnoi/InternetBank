namespace InternetBank.Domain.Exceptions.Account;

public sealed class PassAndRepeatPassIsNotSame : DomainExceptions
{
    public const string Massage = "password and repeat password is not same";
    public const int StatusCodeConst = 400;

    public PassAndRepeatPassIsNotSame()
: base(StatusCodeConst, Massage)
    {


    }
    public PassAndRepeatPassIsNotSame(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

