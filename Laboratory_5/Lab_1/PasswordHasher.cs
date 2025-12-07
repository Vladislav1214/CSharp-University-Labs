using System.Security.Cryptography;
using System.Text;

namespace Lab_1
{
    public class PasswordHasher
    {
        public static string Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }

            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                string hashString = Convert.ToBase64String(hashBytes);

                return hashString;
            }
        }
    }
}
