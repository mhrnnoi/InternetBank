namespace InternetBank.Domain.Exceptions;

public abstract partial class Transaction
{
    public class AlreadyCompletedTransaction : DomainExceptions
    {
        public const string Massage = "this transaction is already completed succesfuly";
        public const int StatusCodeConst = 400;
        public const string TitleConst = "completed transaction ";
        public AlreadyCompletedTransaction(string? massage)
            : base(StatusCodeConst, massage!)
        {


        }

        public AlreadyCompletedTransaction() : base(StatusCodeConst, Massage)
        {
            
        }
    }

}
