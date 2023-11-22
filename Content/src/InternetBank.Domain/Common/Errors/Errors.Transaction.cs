using ErrorOr;

namespace InternetBank.Domain.Common.Errors;

public static partial class Errors
{
    public static class Transaction
    {
        public static Error AlreadyCompletedTransaction => Error.Failure(code: "Transaction.AlreadyCompletedTransaction",
            description: "this transaction is already completed succesfuly");
        public static Error IncorrectAmountRange => Error.Failure(code: "Transaction.IncorrectAmountRange",
            description: "Incorrect Amount Range amount should be betwean 1000 and 5000000");
        public static Error SourceAccountIsBlocked => Error.Failure(code: "Transaction.SourceAccountIsBlocked",
            description: "Source Account is blocked so no transfering can be done both recieve and send");
        public static Error DestinationAccountIsBlocked => Error.Failure(code: "Transaction.DestinationAccountIsBlocked",
            description: "Destination Account is blocked so no transfering can be done both recieve and send");
        public static Error SourceIncorrectCardNumber => Error.Failure(code: "Transaction.SourceIncorrectCardNumber",
            description: "Incorrect source Card Number");
        public static Error DestinationIncorrectCardNumber => Error.Failure(code: "Transaction.DestinationIncorrectCardNumber",
            description: "Incorrect destination Card Number");
        public static Error IncorrectCVV2 => Error.Failure(code: "Transaction.IncorrectCVV2",
            description: "Incorrect CVV2");
        public static Error IncorrectExpiryDate => Error.Failure(code: "Transaction.IncorrectExpiryDate",
            description: "month or year of expiry is incorrect");
        public static Error InsuficcentBalance => Error.Failure(code: "Transaction.InsuficcentBalance",
            description: "you dont have enough amount to transfer");
        public static Error NotYourTransaction => Error.Failure(code: "Transaction.NotYourTransaction",
            description: "you dont have transaction with this information");
        public static Error OtpLimit => Error.Failure(code: "Transaction.OtpLimit",
            description: "you cant request new otp before 2 minutue");
    }
}