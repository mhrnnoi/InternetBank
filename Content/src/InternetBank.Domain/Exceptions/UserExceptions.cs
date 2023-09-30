namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public static class UserExceptions 
    {

        public class FirstNameIsNotFarsi : CustomExceptions
        {
            public const string Massage = "Plz Write first name with persian keyboard";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "First Name Is Not Written Farsi";
            public FirstNameIsNotFarsi()
                :base(Massage)
            {
                
                base.StatusCode = StatusCodeConst;
                base.Title = TitleConst;
                

            }

        }

    }
}
