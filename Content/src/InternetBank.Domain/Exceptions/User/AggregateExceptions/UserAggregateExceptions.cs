namespace InternetBank.Domain.Exceptions.User.AggregateExceptions;

public abstract partial class UserAggregateExceptions : AggregateException
{
    public int StatusCode { get; protected init; }

    public UserAggregateExceptions(string massage, int statusCode, params DomainExceptions[] exceptions) : base(massage, exceptions)
    {
        StatusCode = statusCode;
    }


}