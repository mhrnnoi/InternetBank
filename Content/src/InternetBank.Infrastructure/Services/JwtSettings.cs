namespace InternetBank.Infrastructure.Services;

public class JwtSettings
{
    public const string Key = "JwtSettings";
    public string Secret { get; set; } = null!;
    public int Expiry { get; set; }
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;

}