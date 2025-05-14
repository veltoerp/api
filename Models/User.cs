using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;
public class User
{
    public Guid Id { get; set; } 
    public Guid TenantId { get; set; }
    [Column("role_id")]
    public Guid? RoleId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? TimeZone { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    //[ForeignKey("RoleId")]
    public virtual Role Role { get; set; }
 }   