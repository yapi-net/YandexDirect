using System;
using System.Security.Cryptography;
using System.Text;

namespace Yandex.Direct.Authentication
{
    public class FinanceTokenGenerator
    {
        public string Login { get; private set; }
        public long OperationId { get; private set; }
        public string FinanceToken { get; private set; }

        public FinanceTokenGenerator(string login, string method, string token)
        {
            Login = login;
            OperationId = LongNumber();
            FinanceToken = ComputeHash(string.Format("{0}{1}{2}{3}", token, OperationId, method, Login));
        }

        private static long LongNumber()
        {
            return (long)(DateTime.UtcNow - new DateTime(2010, 1, 1)).TotalSeconds;
        }

        private static string ComputeHash(string input)
        {
            using (var provider = new SHA256CryptoServiceProvider())
            {
                var hash = new StringBuilder();

                var bytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = provider.ComputeHash(bytes);
                
                foreach (var hashByte in hashBytes)
                    hash.Append(hashByte.ToString("x2"));
                
                return hash.ToString();
            }
        }
    }
}