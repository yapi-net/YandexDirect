using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Yandex.Direct.Connectivity;

namespace Yandex.Direct.Authentication
{
    public class StoredCertificateAuthProvider : YandexDirectAuthProviderBase
    {
        private const string YandexIssuerName = "CN=Passport Issuing CA, OU=Passport, O=Yandex, C=RU";

        private volatile X509Certificate2Collection _certificates;
        private readonly object _syncLock = new object();

        public StoredCertificateAuthProvider()
        {
        }

        public StoredCertificateAuthProvider(string login, string masterToken)
            : base(login, masterToken)
        {
        }

        public override void OnHttpRequest(IYandexApiClient client, HttpWebRequest request)
        {
            if (_certificates == null)
            {
                lock (_syncLock)
                {
                    if (_certificates == null)
                    {
                        X509Store store = new X509Store();
                        store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

                        try
                        {
                            _certificates = store.Certificates.Find(X509FindType.FindByIssuerDistinguishedName, YandexIssuerName, false);
                        }
                        finally
                        {
                            store.Close();
                        }
                    }
                }
            }

            request.ClientCertificates = _certificates;
        }
    }
}
