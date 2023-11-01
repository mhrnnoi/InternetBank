namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class User
    {
        public class AlreadyExistPhoneNumber : DomainExceptions
        {
            public const string Massage = "a user with this Phone number is already exist";
            public const int StatusCodeConst = 409;
            public const string TitleConst = "Already Exist Phone Number";
            public AlreadyExistPhoneNumber()
                : base(Massage, StatusCodeConst, TitleConst)
            {


            }

        }

    }
}