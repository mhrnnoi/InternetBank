namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class User
    {
        public class IncorrectPass : DomainExceptions
        {
            public const string Massage = "Incorrect Password";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "Invalid password";
            public IncorrectPass()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}