using ErrorOr;

namespace InternetBank.Domain.Common.Errors;

public static partial class Errors
{
    public static class Account
    {
        public static Error SourceAccountIsBlocked => Error.Failure(code: "Account.SourceAccountIsBlocked",
            description: "Source Account is blocked so no transfering can be done both recieve and send");
        public static Error DestinationAccountIsBlocked => Error.Failure(code: "Account.DestinationAccountIsBlocked",
            description: "Destination Account is blocked so no transfering can be done both recieve and send");
        public static Error AccountIsNotYours => Error.Failure(code: "Account.AccountIsNotYours",
            description: "Account Is Not yours");
        public static Error ExpiredAccount => Error.Failure(code: "Account.ExpiredAccount",
            description: "Account Is Expired");
        public static Error IncorrectPass => Error.Failure(code: "Account.IncorrectPass",
            description: "Incorrect Password");
        public static Error IncorrectPassFormat => Error.Failure(code: "Account.IncorrectPassFormat",
            description: "Incorrect Password format : password should have 6 numeric characters");
        public static Error InvalidAccountType => Error.Failure(code: "Account.InvalidAccountType",
            description: "Incorrect Account type plz enter 1 for saving account or 2 for checking");
        public static Error PassAndRepeatPassIsNotSame => Error.Failure(code: "Account.PassAndRepeatPassIsNotSame",
            description: "password and repeat password is not same");
        public static Error MinimumAccountAmount => Error.Failure(code: "Account.MinimumAccountAmount",
            description: "Amount at least should be 10k");
        public static Error NotFoundAccount => Error.NotFound(code: "Account.NotFound",
            description: "account is not found");
    }
}