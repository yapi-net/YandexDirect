using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Yandex.Direct
{
    [Serializable]
    public class YandexConnectionException : Exception
    {
        public YandexConnectionException()
        {
        }

        public YandexConnectionException(string message)
            : base(message)
        {
        }

        public YandexConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public YandexConnectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
