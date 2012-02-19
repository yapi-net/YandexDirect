using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

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

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        private YapiServerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}