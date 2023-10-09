namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class User 
    {
        public class FirstNameIsNotFarsi : DomainExceptions
        {
            public const string Massage = "Plz Write first name with persian keyboard";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "First Name Is Not Written Farsi";
            public FirstNameIsNotFarsi()
                :base(Massage, StatusCodeConst, TitleConst)
            {
                

            }

        }

    }
}
