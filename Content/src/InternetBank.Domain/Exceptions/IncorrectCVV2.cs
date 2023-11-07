namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class Transaction 
    {
        public class IncorrectCVV2 : DomainExceptions
        {
            public const string Massage = "Incorrect CVV2";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "Invalid CVV2";
            public IncorrectCVV2()
                :base(Massage, StatusCodeConst, TitleConst)
            {
                

            }

        }

    }
}