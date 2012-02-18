using System;
using System.Security.Cryptography;
using System.Text;

namespace Yandex.Direct
{
    public class YandexSignature
    {
        public string Token { get; private set; }
        public long OperationId { get; private set; }
        public string Login { get; private set; }

        public YandexSignature(string masterToken, string method, string login)
        {
            this.Login = login;
            this.OperationId = LongNumber();
            var raw = String.Format("{0}{1}{2}{3}", masterToken, this.OperationId, method, this.Login);
            this.Token = ComputeHash(raw);
        }

        private static long LongNumber()
        {
            return (long)(DateTime.UtcNow - new DateTime(2010, 1, 1)).TotalSeconds;
        }

        private static string ComputeHash(string input)
        {
            var res = new StringBuilder();
            using (var provider = new SHA256CryptoServiceProvider())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var encBytes = provider.ComputeHash(bytes);
                foreach (var encByte in encBytes)
                    res.Append(encByte.ToString("x2"));
                return res.ToString();
            }
        }
    }
}