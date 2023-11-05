namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class Transaction
    {
        public class AccountIsBlocked : DomainExceptions
        {
            public const string Massage = "Account Is Blocked plz unblock first";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "Blocked Account";
            public AccountIsBlocked()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}