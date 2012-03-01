using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Yandex.Direct.Configuration;
using Yandex.Direct.Connectivity;

namespace Yandex.Direct.Tests
{
    [TestClass]
    public class YapiSettingsTest
    {
        [TestMethod, Description("Section yandex.direct can be read from app.config")]
        public void SettingsCanBeReadFromConfiguration()
        {
            var settings = YapiSettings.FromConfiguration();
            settings.CertificatePath.ShouldNotBeNull();
            settings.CertificatePassword.ShouldNotBeNull();
            settings.Language.ShouldBe(YapiLanguage.Russian);
        }
    }
}
