namespace InternetBank.Domain.Exceptions;

public abstract class DomainExceptions : Exception
{
    public int StatusCode { get; protected init; }

    protected DomainExceptions(int statusCode, string massage) : base(massage)
    {
        StatusCode = statusCode;
    }

    
}