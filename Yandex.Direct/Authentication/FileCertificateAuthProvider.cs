using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Yandex.Direct.Connectivity;

namespace Yandex.Direct.Authentication
{
    public class FileCertificateAuthProvider : YandexDirectAuthProviderBase
    {
        private volatile X509Certificate2 _certificate;
        private readonly object _syncLock = new object();

        public string CertificatePath { get; set; }
        public string CertificatePassword { get; set; }

        public FileCertificateAuthProvider()
        {
        }

        public FileCertificateAuthProvider(string certificatePath, string certificatePassword)
        {
            CertificatePath = certificatePath;
            CertificatePassword = certificatePassword;
        }

        public FileCertificateAuthProvider(string certificatePath, string certificatePassword, string login, string masterToken)
            : base(login, masterToken)
        {
            CertificatePath = certificatePath;
            CertificatePassword = certificatePassword;
        }

        public override void LoadSettings(IAuthProviderSettings settings)
        {
            base.LoadSettings(settings);

            CertificatePath = settings["certificatePath"];
            CertificatePassword = settings["certificatePassword"];
        }

        public override void OnHttpRequest(IYandexApiClient client, HttpWebRequest request)
        {
            if (_certificate == null)
            {
                lock (_syncLock)
                {
                    if (_certificate == null)
                        _certificate = new X509Certificate2(CertificatePath, CertificatePassword);
                }
            }

            request.ClientCertificates.Add(_certificate);
        }
    }
}
