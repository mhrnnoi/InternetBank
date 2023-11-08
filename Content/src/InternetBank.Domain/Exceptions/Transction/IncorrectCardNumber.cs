namespace InternetBank.Domain.Exceptions.Transaction;

public sealed class IncorrectCardNumber : DomainExceptions
{
    public const string Massage = "Incorrect Card Number";
    public const int StatusCodeConst = 400;

    public IncorrectCardNumber()
: base(StatusCodeConst, Massage)
    {


    }
    public IncorrectCardNumber(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

