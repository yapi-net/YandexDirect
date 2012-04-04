using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Yandex.Direct.Authentication;

namespace Yandex.Direct.Configuration
{
    public class AuthProviderConfigElement : ConfigurationElementCollection, IAuthProviderSettings
    {
        private readonly ConfigurationProperty _propertyType = new ConfigurationProperty("type", typeof(string), null,
            ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)base[_propertyType];
            }
            set
            {
                this[_propertyType] = value;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new KeyValueConfigurationElement(null, null);
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new KeyValueConfigurationElement(elementName, null);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((KeyValueConfigurationElement)element).Key;
        }

        public new string this[string key]
        {
            get
            {
                var element = (KeyValueConfigurationElement)BaseGet(key);
                return element != null ? element.Value : null;
            }
        }
    }
}
