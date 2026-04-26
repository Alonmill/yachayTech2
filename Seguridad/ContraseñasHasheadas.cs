using System.Security.Cryptography;

namespace YachayTech_p_cov.Seguridad
{
    public class ContraseñasHasheadas
    {
        public static (string Hash, string Salt) HashPassword(string password)
        {
            var saltBytes = RandomNumberGenerator.GetBytes(16);
            var hashBytes = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, 100_000, HashAlgorithmName.SHA256, 32);
            return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
        }

        public static bool VerifyPassword(string password, string hash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var computed = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, 100_000, HashAlgorithmName.SHA256, 32);
            var hashBytes = Convert.FromBase64String(hash);
            return CryptographicOperations.FixedTimeEquals(computed, hashBytes);
        }
    }
}
