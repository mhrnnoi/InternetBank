namespace InternetBank.Domain.Exceptions.Account;

public class AccountExceptions : Exception
{
    public int statusCode { get; set; }
    public string desc { get; set; }
    public AccountExceptions(string massage, int statusCode, string desc) : base()
    {
        this.statusCode = statusCode;
        this.desc = desc;
    }
}