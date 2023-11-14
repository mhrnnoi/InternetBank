namespace InternetBank.Domain.Exceptions.Transaction;
using InternetBank.Domain.Abstracts.Exceptions;

public sealed class IncorrectCVV2 : DomainExceptions
{
    public const string Massage = "Incorrect CVV2";
    public const int StatusCodeConst = 400;

    public IncorrectCVV2()
: base(StatusCodeConst, Massage)
    {


    }
    public IncorrectCVV2(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

