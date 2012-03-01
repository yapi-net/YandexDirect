using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Yandex.Direct.Connectivity;

namespace Yandex.Direct.Authentication
{
    public interface IYandexDirectAuthProvider
    {
        void OnHttpRequest(IYandexApiClient client, HttpWebRequest request);
        void OnRequestMessage(IYandexApiClient client, string method, IDictionary<string, object> messageParams, bool financeTokenRequired);
    }
}
