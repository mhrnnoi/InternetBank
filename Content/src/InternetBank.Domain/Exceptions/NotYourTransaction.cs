namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class Transaction
    {
        public class NotYourTransaction : DomainExceptions
        {
            public const string Massage = "you dont have transaction with this information";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "wrong transaction";
            public NotYourTransaction()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}