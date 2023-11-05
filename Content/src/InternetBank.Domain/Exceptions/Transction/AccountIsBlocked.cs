namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class Transaction
    {
        public class AccountIsBlocked : DomainExceptions
        {
            
            public const int StatusCodeConst = 400;
            public const string TitleConst = "Blocked Account";
            public AccountIsBlocked(string Massage)
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}