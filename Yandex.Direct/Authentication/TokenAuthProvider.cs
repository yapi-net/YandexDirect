using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yandex.Direct.Connectivity;

namespace Yandex.Direct.Authentication
{
    public class TokenAuthProvider : YandexDirectAuthProviderBase
    {
        public string ApplicationId { get; set; }
        public string Token { get; set; }

        public TokenAuthProvider()
        {
        }

        public TokenAuthProvider(string login, string applicationId, string token)
            : base(login, null)
        {
            ApplicationId = applicationId;
            Token = token;
        }
        
        public TokenAuthProvider(string login, string applicationId, string token, string masterToken)
            : base(login, masterToken)
        {
            ApplicationId = applicationId;
            Token = token;
        }

        public override void LoadSettings(IAuthProviderSettings settings)
        {
            base.LoadSettings(settings);

            ApplicationId = settings["applicationId"];
            Token = settings["token"];
        }

        public override void OnRequestMessage(IYandexApiClient client, string method, IDictionary<string, object> messageParams, bool financeTokenRequired)
        {
            base.OnRequestMessage(client, method, messageParams, financeTokenRequired);

            messageParams["login"] = Login;
            messageParams["application_id"] = ApplicationId;
            messageParams["token"] = Token;
        }
    }
}
