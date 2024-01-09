using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ReseñaTuMundo_Api.Data.Utilities
{
    public static class SecurityUtils
    {
        public static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                var inputHash = GetSha256Hash(inputPassword);
                return inputHash == hashedPassword;
            }
        }

        public static string GetSha256Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
