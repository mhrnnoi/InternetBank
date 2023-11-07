namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class Transaction
    {
        public class ExpiredAccount : DomainExceptions
        {
            public const string Massage = "Account Is Expired";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "Expired Account";
            public ExpiredAccount()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}