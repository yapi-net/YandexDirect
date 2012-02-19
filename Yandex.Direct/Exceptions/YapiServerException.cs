using System;
using System.Runtime.Serialization;

namespace Yandex.Direct
{
    [Serializable]
    public sealed class YapiServerException : YapiException
    {
        public YapiServerException()
        { }

        public YapiServerException(string message)
            : base(message)
        { }

        public YapiServerException(string message, Exception inner)
            : base(message, inner)
        { }

        protected YapiServerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}