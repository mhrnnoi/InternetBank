namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class Transaction
    {
        public class AccountIsNotYours : DomainExceptions
        {
            public const string Massage = "Account Is Not yours";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "Not Your Account";
            public AccountIsNotYours()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}