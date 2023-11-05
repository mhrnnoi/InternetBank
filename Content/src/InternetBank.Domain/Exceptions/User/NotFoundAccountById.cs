namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class User
    {
        public class NotFoundAccountById : DomainExceptions
        {
            public const string Massage = "there is no account with this id";
            public const int StatusCodeConst = 404;
            public const string TitleConst = "Not Found";
            public NotFoundAccountById()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}