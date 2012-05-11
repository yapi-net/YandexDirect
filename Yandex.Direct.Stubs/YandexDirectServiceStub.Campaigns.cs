using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Direct
{
    partial class YandexDirectServiceStub
    {
        public List<ShortCampaignInfo> GetClientCampaigns(params string[] logins)
        {
            if (logins == null || logins.Length == 0)
                throw new ArgumentNullException("logins");

            return new List<ShortCampaignInfo>();
        }
    }
}
