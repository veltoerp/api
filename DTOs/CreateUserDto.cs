using System;

namespace api.Dtos
{
    public class CreateUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid TenantId { get; set; } // TenantId'nin Guid olduğunu varsayalım
        // Kullanıcı oluşturma için gereken diğer özellikleri ekleyebilirsiniz
    }
}