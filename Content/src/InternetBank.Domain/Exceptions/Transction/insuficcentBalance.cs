namespace InternetBank.Domain.Exceptions.Transaction;
using InternetBank.Domain.Abstracts.Exceptions;

        public sealed class InsuficcentBalance : DomainExceptions
        {
            public const string Massage = "you dont have enough amount to transfer";
            public const int StatusCodeConst = 400;
            
           public InsuficcentBalance()
        : base(StatusCodeConst, Massage)
    {


    }
    public InsuficcentBalance(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

        }

