using System;

namespace api.Dtos
{
    public class UpdateUserDto
    {
        public string Email { get; set; }
        public Guid TenantId { get; set; } // TenantId'nin Guid olduğunu varsayalım
        // Güncellenebilecek diğer özellikleri ekleyebilirsiniz
        // Şifre güncelleme muhtemelen ayrı bir endpoint olmalıdır
    }
}