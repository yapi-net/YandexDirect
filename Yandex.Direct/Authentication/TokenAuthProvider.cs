using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yandex.Direct.Connectivity;

namespace Yandex.Direct.Authentication
{
    public class TokenAuthProvider : YandexAuthProviderBase
    {
        public override void OnRequestMessage(IYandexApiClient client, string method, IDictionary<string, object> messageParams, bool financeTokenRequired)
        {
            base.OnRequestMessage(client, method, messageParams, financeTokenRequired);

            messageParams["login"] = client.Settings.Login;
            messageParams["application_id"] = client.Settings.ApplicationId;
            messageParams["token"] = client.Settings.Token;
        }
    }
}
