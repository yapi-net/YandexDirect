using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Yandex.Direct.Connectivity;

namespace Yandex.Direct
{
    [Serializable]
    public sealed class YapiCodeServerException : YapiServerException
    {
        const string ErrorCodeField = "ErrorCode";
        public YandexApiErrorCode ErrorCode { get; private set; }

        public YapiCodeServerException(YandexApiErrorCode errorCode, string error)
            : base(string.Format("Yandex API error, code {0}. {1}", errorCode, error))
        {
            ErrorCode = errorCode;
        }

        public YapiCodeServerException()
        { }

        public YapiCodeServerException(string message)
            : base(message)
        { }

        public YapiCodeServerException(string message, Exception inner)
            : base(message, inner)
        { }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private YapiCodeServerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrorCode = (YandexApiErrorCode)info.GetValue(ErrorCodeField, typeof(YandexApiErrorCode));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            info.AddValue(ErrorCodeField, ErrorCode);
            base.GetObjectData(info, context);
        }
    }
}