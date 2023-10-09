namespace InternetBank.Domain.Exceptions.User.AggregateExceptions;

public abstract partial class UserAggregateExceptions
{
    public class CreateUser : UserAggregateExceptions
    {
        public const string Massage = "there is validation erors when trying to create a user";
        public const int StatusCodeConst = 400;
        public CreateUser(params DomainExceptions[] exceptions ) : base(Massage, StatusCodeConst, exceptions)
        {

        }
    }


}