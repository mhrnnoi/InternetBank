namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class User
    {
        public class IncorrectNationalCode : DomainExceptions
        {
            public const string Massage = "Plz Write Correct National Code";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "National Code Is not Correct";
            public IncorrectNationalCode()
                : base(Massage, StatusCodeConst, TitleConst)
            {

             


            }

        }
    }
}
