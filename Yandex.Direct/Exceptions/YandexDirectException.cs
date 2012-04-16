using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Yandex.Direct.Connectivity;

namespace Yandex.Direct
{
    [Serializable]
    public class YandexDirectException : Exception
    {
        public YandexApiErrorCode ErrorCode { get; private set; }

        public YandexDirectException()
        {
        }

        public YandexDirectException(string message)
            : base(message)
        {
        }

        public YandexDirectException(YandexApiErrorCode errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public YandexDirectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrorCode = (YandexApiErrorCode)info.GetInt32("ErrorCode");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ErrorCode", (int)ErrorCode);
            base.GetObjectData(info, context);
        }
    }
}
