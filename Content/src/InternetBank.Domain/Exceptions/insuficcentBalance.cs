namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class Transaction
    {
        public class insuficcentBalance : DomainExceptions
        {
            public const string Massage = "you dont have enough amount to transfer";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "insuficcent Balance";
            public insuficcentBalance()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}