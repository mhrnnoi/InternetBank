namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class User
    {
        public class AlreadyExistNationalCode : DomainExceptions
        {
            public const string Massage = "a user with this national code is already exist";
            public const int StatusCodeConst = 409;
            public const string TitleConst = "Already Exist National Code";
            public AlreadyExistNationalCode()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}