using ErrorOr;

namespace InternetBank.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error InvalidCred => Error.Unauthorized(
                                                          code: "User.InvalidCred",
                                                          description: "Invalid Credential");
        public static Error AlreadyExistNationalCode => Error.Conflict(
                                                          code: "User.AlreadyExistNationalCode",
                                                          description: "a user with this national code is already exist");
        public static Error AlreadyExistPhoneNumber => Error.Conflict(
                                                          code: "User.AlreadyExistPhoneNumber",
                                                          description: "a user with this Phone number is already exist");
        public static Error Below18 => Error.Failure(
                                                          code: "User.Below18",
                                                          description: "you're not in supported age range ");
        public static Error FirstNameIsNotFarsi => Error.Failure(
                                                          code: "User.FirstNameIsNotFarsi",
                                                          description: "Plz Write first name with persian keyboard");
        public static Error IncorrectNationalCode => Error.Failure(
                                                          code: "User.IncorrectNationalCode",
                                                          description: "Plz Write Correct National Code");
        public static Error LastNameIsNotFarsi => Error.Failure(
                                                          code: "User.LastNameIsNotFarsi",
                                                          description: "Plz Write Last name with persian keyboard");
        public static Error NotYourAccount => Error.NotFound(
                                                          code: "User.NotYourAccount",
                                                          description: "you dont have any account with this id");
        public static Error NotFoundAccountById => Error.NotFound(
                                                          code: "User.NotFoundAccountById",
                                                          description: "there is no account with this id");
        public static Error NotFoundUserById => Error.NotFound(
                                                          code: "User.NotFoundUserById",
                                                          description: "there is no user with this id");


    }
}