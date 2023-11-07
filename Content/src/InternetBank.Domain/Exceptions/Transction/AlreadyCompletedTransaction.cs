namespace InternetBank.Domain.Exceptions.Transaction;

public class AlreadyCompletedTransaction : DomainExceptions
{
    public const string Massage = "this transaction is already completed succesfuly";
    public const int StatusCodeConst = 400;
    public AlreadyCompletedTransaction()
        : base(StatusCodeConst, Massage)
    {


    }
    public AlreadyCompletedTransaction(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }
}
