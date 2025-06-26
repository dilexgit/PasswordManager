using System;
using System.Linq;
using System.Security.Cryptography;
namespace PasswordManagerMVC.Utilities
{
    public static class PasswordGenerator
    {
        public static void GenerateAndPrintKeys()
        {
            using (Aes aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();

                var key = Convert.ToBase64String(aes.Key);
                var iv = Convert.ToBase64String(aes.IV);
            }
        }


        private static readonly Random Random = new Random();
        private const string Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+";

        public static string Generate(int length)
        {
            return new string(Enumerable.Repeat(Chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
