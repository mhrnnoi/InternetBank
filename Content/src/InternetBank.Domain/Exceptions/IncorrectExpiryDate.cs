namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class Transaction
    {
        public class IncorrectExpiryDate : DomainExceptions
        {
            public const string Massage = "month or year of expiry is incorrect";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "wrong expiry date";
            public IncorrectExpiryDate()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}