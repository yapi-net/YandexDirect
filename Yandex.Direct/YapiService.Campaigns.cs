using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Direct
{
    partial class YapiService
    {
        /*
         * Not implemented:
           - CreateOrUpdateCampaign
           - GetBalance
           - GetCampaignParams
           - GetCampaignsList
           - GetCampaignsListFilter
           - GetCampaignsParams
         */

        public List<ShortCampaignInfo> GetClientCampaigns(params string[] logins)
        {
            if (logins == null || logins.Length == 0)
                throw new ArgumentNullException("logins");

            return YandexApiClient.Invoke<List<ShortCampaignInfo>>(ApiMethod.GetCampaignsList, logins);
        }
    }
}
