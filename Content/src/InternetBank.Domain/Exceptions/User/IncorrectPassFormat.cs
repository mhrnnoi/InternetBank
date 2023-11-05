namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class User
    {
        public class IncorrectPassFormat : DomainExceptions
        {
            public const string Massage = "Incorrect Password format : password should have 6 numeric characters";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "Invalid password format";
            public IncorrectPassFormat()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}