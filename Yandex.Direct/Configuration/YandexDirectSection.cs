using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Yandex.Direct.Configuration
{
    public class YandexDirectSection : ConfigurationSection
    {
        [ConfigurationProperty("serviceUrl", IsRequired = false)]
        public string ServiceUrl
        {
            get
            {
                return (string)this["serviceUrl"];
            }
            set
            {
                this["serviceUrl"] = value;
            }
        }

        [ConfigurationProperty("language", IsRequired = false)]
        public YandexApiLanguage? Language
        {
            get
            {
                return (YandexApiLanguage?)this["language"];
            }
            set
            {
                this["language"] = value;
            }
        }

        [ConfigurationProperty("authProvider", IsRequired = false)]
        public AuthProviderConfigElement AuthProvider
        {
            get
            {
                return (AuthProviderConfigElement)this["authProvider"];
            }
            set
            {
                this["authProvider"] = value;
            }
        }
    }
}