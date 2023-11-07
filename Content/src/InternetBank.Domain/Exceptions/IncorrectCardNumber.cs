namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions
{
    public abstract partial class Transaction 
    {
        public class IncorrectCardNumber : DomainExceptions
        {
            public const string Massage = "Incorrect Card Number";
            public const int StatusCodeConst = 400;
            public const string TitleConst = "There is no account with that card number";
            public IncorrectCardNumber()
                :base(Massage, StatusCodeConst, TitleConst)
            {
                

            }

        }

    }
}