using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Yandex.Direct.Authentication;
using Yandex.Direct.Configuration;
using Yandex.Direct.Serialization;

namespace Yandex.Direct.Connectivity
{
    public class JsonYandexApiClient : IYandexApiClient
    {
        public YapiSettings Settings { get; private set; }

        private readonly JsonYandexApiSerializer _serializer;
        private readonly IYandexAuthProvider _authProvider;

        public JsonYandexApiClient(YapiSettings settings, IYandexAuthProvider authProvider)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            Settings = settings;

            _serializer = new JsonYandexApiSerializer();
            _authProvider = authProvider;
        }

        public T Invoke<T>(string method, object param = null, bool financeTokenRequired = false)
        {
            if (string.IsNullOrEmpty(method))
                throw new ArgumentNullException("method");

            // Creating http-request

            var requestMessage = CreateRequestMessage(method, param, financeTokenRequired);
            var request = CreateRequest(requestMessage);

            // Invoking the request to server

            string responseMessage;

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    responseMessage = GetResponseMessage(response);
                }
            }
            catch (Exception ex)
            {
                throw new YapiServerException("Unable to perform http-request to Yandex API endpoint.", ex);
            }

            // Retrieving response

            var responseObject = _serializer.Deserialize<JsonResponseObject<T>>(responseMessage);

            if (responseObject == null)
                throw new YapiServerException("Not supported server response.");

            if (responseObject.ErrorCode != YandexApiErrorCode.None)
                throw new YapiCodeServerException(responseObject.ErrorCode, responseObject.ErrorMessage);

            return responseObject.Object;
        }

        private string CreateRequestMessage(string method, object param, bool financeTokenRequired)
        {
            // Creating request message

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters["method"] = method;
            parameters["param"] = param;
            parameters["locale"] = Settings.Language;

            // Adding authentication information into the request message

            if (_authProvider != null)
                _authProvider.OnRequestMessage(this, method, parameters, financeTokenRequired);

            return SerializeRequestMessage(parameters);
        }

        private HttpWebRequest CreateRequest(string requestMessage)
        {
            var request = (HttpWebRequest)WebRequest.Create(Settings.ApiAddress);

            // Adding authentication information into the http-request

            if (_authProvider != null)
                _authProvider.OnHttpRequest(this, request);
            
            // Configuring request

            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            var data = Encoding.UTF8.GetBytes(requestMessage);

            request.ContentLength = data.Length;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }

            return request;
        }

        private static string GetResponseMessage(WebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader responseStream = new StreamReader(stream))
                {
                    return responseStream.ReadToEnd();
                }
            }
        }

        private string SerializeRequestMessage(Dictionary<string, object> messageParams)
        {
            var serializedParams = messageParams.Where(param => param.Value != null).Select(param =>
            {
                string serializedValue = _serializer.Serialize(param.Value);

                return string.Format("\"{0}\": {1}", param.Key, serializedValue);
            });

            return string.Format("{{{0}}}", string.Join(", ", serializedParams));
        }
    }
}