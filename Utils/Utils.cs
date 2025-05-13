using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
namespace api.Utils;

public static class Helper
{
    // genarete hash password
    public static string HashPassword(string password)
    {
       return BCrypt.Net.BCrypt.HashPassword(password);
    }
    // chesk password
    public static bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}