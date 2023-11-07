namespace InternetBank.Domain.Exceptions;


public abstract partial class Transaction
{
    public class WrongOTP : DomainExceptions
    {
        public const string Massage = "your entered otp is wrong or maybe expired";
        public const int StatusCodeConst = 400;
        public const string TitleConst = "wrong otp";
        public WrongOTP()
            : base(Massage, StatusCodeConst, TitleConst)
        {


        }

    }

}
