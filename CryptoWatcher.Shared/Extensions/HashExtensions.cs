using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptoWatcher.Shared.Extensions
{
    public static class HashExtensions
    {
        public static string Sha256(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using (var shA256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return Convert.ToBase64String(shA256.ComputeHash(bytes));
            }
        }
    }
}