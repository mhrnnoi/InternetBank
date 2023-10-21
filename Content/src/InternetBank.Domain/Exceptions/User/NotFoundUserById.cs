namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class User
    {
        public class NotFoundUserById : DomainExceptions
        {
            public const string Massage = "there is no user with this id";
            public const int StatusCodeConst = 404;
            public const string TitleConst = "Not Found";
            public NotFoundUserById()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}