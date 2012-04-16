using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Direct
{
    partial class YandexDirectService
    {
        //TODO: Should be renamed to GetClientsList
        public List<ShortClientInfo> GetClientLogins()
        {
            return YandexApiClient.Invoke<List<ShortClientInfo>>(ApiMethod.GetClientsList);
        }

        public ClientUnitInfo[] GetClientsUnits(params string[] logins)
        {
            if (logins == null || logins.Length == 0)
                throw new ArgumentNullException("logins");

            return YandexApiClient.Invoke<ClientUnitInfo[]>(ApiMethod.GetClientsUnits, logins);
        }
    }
}
