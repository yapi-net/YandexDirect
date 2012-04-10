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
            // Setting defaults

            ServiceUrl = new Uri("https://soap.direct.yandex.ru/json-api/v4/");
            Language = YandexApiLanguage.English;

            // Trying to load settings from configuration file

            LoadFromConfigurationFile();
        }

        public YandexDirectConfiguration(IYandexApiAuthProvider authProvider)
            : this()
        {
            AuthProvider = authProvider;
        }

        public YandexDirectConfiguration(IYandexApiAuthProvider authProvider, YandexApiLanguage language)
            : this()
        {
            AuthProvider = authProvider;
            Language = language;
        }

        private void LoadFromConfigurationFile()
        {
            // Obtaining configuration section

            var configSection = (YandexDirectSection)ConfigurationManager.GetSection("yandex.direct");

            if (configSection == null)
                return;

            // Loading ServiceUrl and Language if specified

            if (!string.IsNullOrEmpty(configSection.ServiceUrl))
                ServiceUrl = new Uri(configSection.ServiceUrl);

            if (configSection.Language != null)
                Language = configSection.Language.Value;

            // Loading authentication provider if specified

            if (configSection.AuthProvider.Type != null)
            {
                var providerType = Type.GetType(configSection.AuthProvider.Type, true);
                var authProvider = (IYandexApiAuthProvider)Activator.CreateInstance(providerType);

                authProvider.LoadSettings(configSection.AuthProvider);

                AuthProvider = authProvider;
            }
        }
    }
}
