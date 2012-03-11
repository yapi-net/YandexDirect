using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Yandex.Direct.Connectivity;

namespace Yandex.Direct.Authentication
{
    public abstract class YandexDirectAuthProviderBase : IYandexDirectAuthProvider
    {
        public virtual void OnHttpRequest(IYandexApiClient client, HttpWebRequest request)
        {
        }

        public virtual void OnRequestMessage(IYandexApiClient client, string method, IDictionary<string, object> messageParams, bool financeTokenRequired)
        {
            if (financeTokenRequired)
            {
                if (string.IsNullOrEmpty(client.Settings.Login))
                    throw new InvalidOperationException("Unable to call finance operation, because authentication login not set in configuration.");

                if (string.IsNullOrEmpty(client.Settings.MasterToken))
                    throw new InvalidOperationException("Unable to call finance operation, because authentication master token not set in configuration.");

                FinanceTokenGenerator tokenGenerator = new FinanceTokenGenerator(client.Settings.Login, method, client.Settings.MasterToken);

                messageParams["login"] = tokenGenerator.Login;
                messageParams["operation_num"] = tokenGenerator.OperationId;
                messageParams["finance_token"] = tokenGenerator.FinanceToken;
            }
        }
    }
}
