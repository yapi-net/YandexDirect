using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Yandex.Direct.Connectivity;

namespace Yandex.Direct.Authentication
{
    public class CertificateFileAuthProvider : YandexAuthProviderBase
    {
        private volatile X509Certificate2 _certificate;
        private readonly object _syncLock = new object();

        public override void OnHttpRequest(IYandexApiClient client, HttpWebRequest request)
        {
            if (_certificate == null)
            {
                lock (_syncLock)
                {
                    if (_certificate == null)
                        _certificate = new X509Certificate2(client.Settings.CertificatePath, client.Settings.CertificatePassword);
                }
            }

            request.ClientCertificates.Add(_certificate);
        }
    }
}
