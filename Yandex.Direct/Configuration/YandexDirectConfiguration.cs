using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Yandex.Direct.Authentication;

namespace Yandex.Direct.Configuration
{
    public class YandexDirectConfiguration : IYandexApiConfiguration
    {
        public Uri ServiceUrl { get; set; }
        public YandexApiLanguage Language { get; set; }

        public IYandexApiAuthProvider AuthProvider { get; set; }

        public YandexDirectConfiguration()
        {
            ServiceUrl = new Uri("https://soap.direct.yandex.ru/json-api/v4/");
            Language = YandexApiLanguage.English;
        }

        public YandexDirectConfiguration(IYandexApiAuthProvider authProvider)
        {
            ServiceUrl = new Uri("https://soap.direct.yandex.ru/json-api/v4/");
            Language = YandexApiLanguage.English;

            AuthProvider = authProvider;
        }

        public static YandexDirectConfiguration LoadFromConfigurationFile()
        {
            // Obtaining configuration section from the configuration file

            var configSection = (YandexDirectSection)ConfigurationManager.GetSection("yandex.direct");

            // Creating and configuring authentication provider if specified

            IYandexApiAuthProvider authProvider;

            if (configSection.AuthProvider.Type != null)
            {
                var providerType = Type.GetType(configSection.AuthProvider.Type, true);
                authProvider = (IYandexApiAuthProvider)Activator.CreateInstance(providerType);

                authProvider.LoadSettings(configSection.AuthProvider);
            }
            else
                authProvider = null;

            // Creating configuration object

            var configuration = new YandexDirectConfiguration(authProvider);

            if (!string.IsNullOrEmpty(configSection.ServiceUrl))
                configuration.ServiceUrl = new Uri(configSection.ServiceUrl);

            if (configSection.Language != null)
                configuration.Language = configSection.Language.Value;

            return configuration;
        }
    }
}
