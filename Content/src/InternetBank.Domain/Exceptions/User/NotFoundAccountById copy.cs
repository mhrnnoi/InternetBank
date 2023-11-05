namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class User
    {
        public class NotYourAccount : DomainExceptions
        {
            public const string Massage = "you dont have any account with this id";
            public const int StatusCodeConst = 404;
            public const string TitleConst = "Incorrect account id";
            public NotYourAccount()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}