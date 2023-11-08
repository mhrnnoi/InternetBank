namespace InternetBank.Domain.Exceptions.Transaction;

public sealed class WrongOTP : DomainExceptions
{
    public const string Massage = "your entered otp is wrong or maybe expired";
    public const int StatusCodeConst = 400;

    public WrongOTP()
: base(StatusCodeConst, Massage)
    {


    }
    public WrongOTP(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

