namespace InternetBank.Domain.Exceptions;

public abstract partial class DomainExceptions : Exception
{
    public int StatusCode { get; protected init; }
    public string Title { get; protected init; }
    protected DomainExceptions(string massage, int statusCode, string title) : base(massage)
    {
        StatusCode = statusCode;
        Title = title;
    }
}