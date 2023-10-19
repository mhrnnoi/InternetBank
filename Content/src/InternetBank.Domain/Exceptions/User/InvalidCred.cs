namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class User 
    {
        public class InvalidCred : DomainExceptions
        {
            public const string Massage = "Incorrect email or Password";
            public const int StatusCodeConst = 401;
            public const string TitleConst = "Invalid cred";
            public InvalidCred()
                :base(Massage, StatusCodeConst, TitleConst)
            {
                

            }

        }

    }
}