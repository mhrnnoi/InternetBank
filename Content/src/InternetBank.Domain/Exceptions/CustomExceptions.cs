namespace InternetBank.Domain.Exceptions;

public abstract class CustomExceptions : Exception
{
    public int StatusCode { get; protected init; }
    public string Title { get; protected init; }
    public CustomExceptions(string massage)
    : base(massage)
    {

    }


}