using System;
using System.Runtime.Serialization;

namespace Yandex.Direct
{
    [Serializable]
    public abstract class YapiException : Exception
    {
        protected YapiException()
        { }

        protected YapiException(string message)
            : base(message)
        { }

        protected YapiException(string message, Exception inner)
            : base(message, inner)
        { }

        protected YapiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}