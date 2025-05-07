
public class Tenant
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string? CountryCode { get; set; }
    public string? CurrencyCode { get; set; }
    public string TimeZone { get; set; } = "UTC";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; } 
}