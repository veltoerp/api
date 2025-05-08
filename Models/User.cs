namespace api.Models;
public class User
{
    public Guid Id { get; set; } 
    public Guid TenantId { get; set; }
    public Guid? RoleId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? TimeZone { get; set; } // kullanıcıya özel timezone (isteğe bağlı)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
 }   