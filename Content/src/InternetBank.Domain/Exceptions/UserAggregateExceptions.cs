namespace InternetBank.Domain.Exceptions.User.AggregateExceptions;
public class UserAggregateExceptions : Exception
{
    public int statusCode { get; set; }
    public string desc { get; set; }
    public UserAggregateExceptions(string massage, int statusCode, string desc) : base()
    {
        this.statusCode = statusCode;
        this.desc = desc;
    }
}