using System;
using System.Runtime.Serialization;

namespace Yandex.Direct
{
    [Serializable]
    public class YapiConfigurationException : YapiException
    {
        public YapiConfigurationException()
        { }

        public YapiConfigurationException(string message)
            : base(message)
        { }

        public YapiConfigurationException(string message, Exception inner)
            : base(message, inner)
        { }

        protected YapiConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}