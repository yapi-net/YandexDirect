using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Yandex.Direct.Serialization;

namespace Yandex.Direct
{
    internal sealed class YapiTransport
    {
        public YapiSettings Setting { get; private set; }
        private readonly YapiSerializer _serializer;

        public YapiTransport(YapiSettings yapiSettings)
        {
            this.Setting = yapiSettings;
            this._serializer = new YapiSerializer();
        }

        public T Request<T>(YapiService.ApiCommand method, object requestData = null, bool sign = false)
        {
            Contract.Requires(!sign || !string.IsNullOrWhiteSpace(this.Setting.MasterToken), "Financial operations require MasterToken to be set");
            Contract.Requires(!sign || !string.IsNullOrWhiteSpace(this.Setting.Login), "Financial operations require Login to be set");

            if (!Enum.IsDefined(typeof(YapiService.ApiCommand), method))
                throw new InvalidEnumArgumentException("method", (int)method, typeof(YapiService.ApiCommand));

            var postMessage = CreatePostMessage(method.ToString(), requestData, sign);
            var replyJsonString = HttpRequest(postMessage);

            var error = _serializer.Deserialize<YandexErrorInfo>(replyJsonString);
            if (error != null && error.Code != YapiService.YapiErrorCode.None)
            {
                // If we can restore error code throw exception with code
                if (Enum.IsDefined(typeof(YapiService.YapiErrorCode), error.Code))
                    throw new YapiCodeServerException(error.Code, error.Error);
                // Otherwise throw more generic exception
                throw new YapiServerException(error.Error);
            }

            var response = _serializer.Deserialize<YapiResponse<T>>(replyJsonString);
            return response.Data;
        }

        #region Sending http request to Yandex API

        private string HttpRequest(string message)
        {
            var request = (HttpWebRequest)WebRequest.Create(this.Setting.ApiAddress);
            if (this.Setting.AuthType == YapiAuthType.Certificate)
                request.ClientCertificates.Add(new X509Certificate2(this.Setting.CertificatePath, this.Setting.CertificatePassword));
            request.Method = "POST";
            request.Proxy = null;
            request.ContentType = "application/json; charset=utf-8";

            var data = Encoding.UTF8.GetBytes(message);

            request.ContentLength = data.Length;
            using (var requestStream = request.GetRequestStream())
                requestStream.Write(data, 0, data.Length);

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var responseStream = new StreamReader(stream))
                return responseStream.ReadToEnd();
        }

        #endregion

        #region Creating json-formatted request body

        private string CreatePostMessage(string method, object requestData, bool sign)
        {
            var request = new YapiRequestBuilder();
            request.AddParameter("method", method);
            request.AddParameter("locale", _serializer.Serialize(this.Setting.Language));

            if (sign)
            {
                var signature = new YandexSignature(this.Setting.MasterToken, method, this.Setting.Login);
                request.AddParameter("finance_token", signature.Token);
                request.AddParameter("operation_num", signature.OperationId);
                request.AddParameter("login", signature.Login);
            }

            if (this.Setting.AuthType == YapiAuthType.Token)
            {
                request.AddParameter("login", this.Setting.Login);
                request.AddParameter("application_id", this.Setting.ApplicationId);
                request.AddParameter("token", this.Setting.Token);
            }

            var parametersJson = requestData == null ? null : _serializer.Serialize(requestData);
            request.AddParameter("param", parametersJson, dontEscapeArray: true);

            return request.BuildRequestBody();
        }

        #endregion
    }
}