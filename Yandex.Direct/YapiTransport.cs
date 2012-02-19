using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Direct
{
    internal sealed class YapiTransport
    {
        public JsonSerializerSettings JsonSettings { get; private set; }
        public YapiSettings Setting { get; private set; }

        public YapiTransport(YapiSettings yapiSettings)
        {
            this.Setting = yapiSettings;
            this.JsonSettings =
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                        Converters = {new IsoDateTimeConverter {DateTimeFormat = "yyyy-MM-dd"}}
                    };
        }

        public T Request<T>(YapiService.ApiCommand method, object requestData = null, bool sign = false)
        {
            if (!Enum.IsDefined(typeof(YapiService.ApiCommand), method))
                throw new InvalidEnumArgumentException("method", (int)method, typeof(YapiService.ApiCommand));

            var json = HttpRequest(method.ToString(), requestData == null ? null : SerializeToJson(requestData), sign);
            var error = JsonConvert.DeserializeObject<YandexErrorInfo>(json, JsonSettings);
            if (error != null && error.Code != 0)
                throw new ApplicationException(error.Error);
            var response = JsonConvert.DeserializeObject<YapiResponse<T>>(json, JsonSettings);
            return response.Data;
        }

        #region Sending http request to Yandex API

        private string HttpRequest(string method, string parametersJson, bool sign = false)
        {
            Contract.Requires(!sign || !string.IsNullOrWhiteSpace(this.Setting.MasterToken), "Financial operations require MasterToken to be set");
            Contract.Requires(!sign || !string.IsNullOrWhiteSpace(this.Setting.Login), "Financial operations require Login to be set");

            var request = (HttpWebRequest)WebRequest.Create(this.Setting.ApiAddress);
            request.ClientCertificates.Add(new X509Certificate2(this.Setting.CertificatePath, this.Setting.CertificatePassword));
            request.Method = "POST";
            request.Proxy = null;
            request.ContentType = "application/json; charset=utf-8";

            var message = CreatePostMessage(method, parametersJson, sign);
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

        private string CreatePostMessage(string method, string parametersJson, bool sign)
        {
            var da = new YapiRequestBuilder();
            da.Add("method", method);
            da.Add("locale", SerializeToJson(this.Setting.Language));

            if (sign)
            {
                var signature = new YandexSignature(this.Setting.MasterToken, method, this.Setting.Login);
                da.Add("finance_token", signature.Token);
                da.Add("operation_num", signature.OperationId);
                da.Add("login", signature.Login);
            }

            da.Add("param", parametersJson, dontEscapeArray: true);

            return da.BuildRequestBody();
        }

        private string SerializeToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, JsonSettings);
        }

        #endregion
    }
}