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
        public IYandexApiConfiguration Configuration { get; private set; }

        private readonly JsonYandexApiSerializer _serializer;

        public JsonYandexApiClient(IYandexApiConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            _serializer = new JsonYandexApiSerializer();
            Configuration = configuration;
        }

        public T Invoke<T>(string method, object param = null, bool financeTokenRequired = false)
        {
            if (string.IsNullOrEmpty(method))
                throw new ArgumentNullException("method");

            // Creating http-request

            var authProvider = Configuration.AuthProvider;

            var requestMessage = CreateRequestMessage(method, param, authProvider, financeTokenRequired);
            var request = CreateRequest(requestMessage, authProvider);

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
                throw new YandexConnectionException("Unable to perform http-request to Yandex API endpoint.", ex);
            }

            // Retrieving response

            var responseObject = _serializer.Deserialize<JsonResponseObject<T>>(responseMessage);

            if (responseObject == null)
                throw new YandexConnectionException("Not supported server response.");

            if (responseObject.ErrorCode != YandexApiErrorCode.None)
                throw new YandexDirectException(responseObject.ErrorCode, responseObject.ErrorMessage);

            return responseObject.Object;
        }

        private string CreateRequestMessage(string method, object param, IYandexApiAuthProvider authProvider,
            bool financeTokenRequired)
        {
            // Creating request message

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters["method"] = method;
            parameters["param"] = param;
            parameters["locale"] = Configuration.Language;

            // Adding authentication information into the request message

            if (authProvider != null)
                authProvider.OnRequestMessage(this, method, parameters, financeTokenRequired);

            return SerializeRequestMessage(parameters);
        }

        private HttpWebRequest CreateRequest(string requestMessage, IYandexApiAuthProvider authProvider)
        {
            var request = (HttpWebRequest)WebRequest.Create(Configuration.ServiceUrl);

            // Adding authentication information into the http-request

            if (authProvider != null)
                authProvider.OnHttpRequest(this, request);
            
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