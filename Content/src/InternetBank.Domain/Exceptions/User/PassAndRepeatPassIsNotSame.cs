namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class User
    {
        public class PassAndRepeatPassIsNotSame : DomainExceptions
        {
            public const string Massage = "password and repeat password is not same";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "repeat pass and pass is not equal";
            public PassAndRepeatPassIsNotSame()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}