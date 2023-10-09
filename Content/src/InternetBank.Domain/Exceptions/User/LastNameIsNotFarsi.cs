namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class User
    {
        public class LastNameIsNotFarsi : DomainExceptions
        {
            public const string Massage = "Plz Write Last name with persian keyboard";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "Last Name Is Not Written Farsi";
            public LastNameIsNotFarsi()
                : base(Massage, StatusCodeConst, TitleConst)
            {

            }

        }
    }
}
