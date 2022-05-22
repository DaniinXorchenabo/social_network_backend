using System.ComponentModel.DataAnnotations;
using BCrypt.Net;

namespace socialNetworkApp.api.controllers.auth;

public static class Security
{
    public static readonly int Cost = 12;
    public static readonly HashType CurrentHashType = HashType.SHA384;

    public static string GetHashedPassword(string password)
    {
        var hashed = BCrypt.Net.BCrypt.EnhancedHashPassword(password, CurrentHashType, workFactor: Cost);
        if (hashed != null)
        {
            return hashed;
        }

        throw new ValidationException("При хешировании пароля возникла ошибка");
    }

    public static bool Verify(string password, string hashed)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashed, CurrentHashType);
    }
}