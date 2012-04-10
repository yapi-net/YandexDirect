using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Yandex.Direct.Authentication;
using Yandex.Direct.Configuration;

namespace Yandex.Direct.Tests
{
    [TestClass]
    public class YandexDirectConfigurationFixture
    {
        [TestMethod]
        public void SettingsCanBeReadFromConfiguration()
        {
            var configuration = new YandexDirectConfiguration();

            Assert.AreEqual(new Uri("http://serviceUrl/"), configuration.ServiceUrl);
            Assert.AreEqual(YandexApiLanguage.Ukrainian, configuration.Language);

            Assert.IsInstanceOfType(configuration.AuthProvider, typeof(FileCertificateAuthProvider));

            Assert.AreEqual("loginValue", ((FileCertificateAuthProvider)configuration.AuthProvider).Login);
            Assert.AreEqual("tokenValue", ((FileCertificateAuthProvider)configuration.AuthProvider).MasterToken);
            Assert.AreEqual("pathValue", ((FileCertificateAuthProvider)configuration.AuthProvider).CertificatePath);
            Assert.AreEqual(null, ((FileCertificateAuthProvider)configuration.AuthProvider).CertificatePassword);
        }
    }
}
