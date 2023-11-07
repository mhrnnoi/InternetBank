namespace InternetBank.Domain.Exceptions.Transaction;

        public class insuficcentBalance : DomainExceptions
        {
            public const string Massage = "you dont have enough amount to transfer";
            public const int StatusCodeConst = 400;
            
           public insuficcentBalance()
        : base(StatusCodeConst, Massage)
    {


    }
    public insuficcentBalance(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

        }

