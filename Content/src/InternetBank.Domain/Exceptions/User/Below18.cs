namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class User
    {
        public class Below18 : DomainExceptions
        {
            public const string Massage = "you're not in supported age range ";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "below 18 !";
            public Below18()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}
