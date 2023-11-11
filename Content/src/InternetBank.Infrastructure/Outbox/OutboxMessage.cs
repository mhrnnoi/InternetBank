namespace InternetBank.Infrastructure.OutboxMessages;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public bool IsProcced { get; set; }
    public DateTime OccuredOnUTC { get; set; }
    public string  Type { get; set; }
    public string  Content { get; set; }
    public DateTime? ProccesedOnUTC { get; set; }
    public string? Error { get; set; }

    public OutboxMessage(string type, string  content)
    {
        Content = content;
        OccuredOnUTC = DateTime.UtcNow;
        IsProcced = false;
        Id = Guid.NewGuid();
        Type = type;
    }
    // public int MyProperty { get; set; }
    // public int MyProperty { get; set; }
    // public int MyProperty { get; set; }
    // public int MyProperty { get; set; }
}