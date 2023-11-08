namespace InternetBank.Domain.Exceptions.Transaction;

public sealed class IncorrectAmountRange : DomainExceptions
{
    public const string Massage = "Incorrect Amount Range amount should be betwean 1000 and 5000000";
    public const int StatusCodeConst = 400;
    public IncorrectAmountRange()
        : base(StatusCodeConst, Massage)
    {


    }
    public IncorrectAmountRange(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }
}
