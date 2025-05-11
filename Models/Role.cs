
namespace api.Models;
public class Role
{
    public Guid Id { get; set; } 
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
}
