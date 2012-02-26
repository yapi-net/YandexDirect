using System;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Yandex.Direct
{
    [Serializable]
    public sealed class YapiCodeServerException : YapiServerException
    {
        const string ErrorCodeField = "ErrorCode";
        public YapiService.YapiErrorCode ErrorCode { get; private set; }

        public YapiCodeServerException(YapiService.YapiErrorCode errorCode, string error)
            : base(string.Format("Yandex API error, code {0}. {1}", errorCode, error))
        {
            this.ErrorCode = errorCode;
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
            this.ErrorCode = (YapiService.YapiErrorCode)info.GetValue(ErrorCodeField, typeof(YapiService.YapiErrorCode));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Contract.Requires<ArgumentNullException>(info != null, "info");
            info.AddValue(ErrorCodeField, this.ErrorCode);
            base.GetObjectData(info, context);
        }
    }
}