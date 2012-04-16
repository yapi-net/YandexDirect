using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Yandex.Direct
{
    [DebuggerDisplay("{BannerId}: {Title}")]
    public abstract class BannerInfoBase
    {
        [JsonProperty("BannerID")]
        public int BannerId { get; set; }

        [JsonProperty("CampaignID")]
        public int CampaignId { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        public string Href { get; set; }

        //TODO: Convert to list of regions (or at leaset of region ids) (Issue #6)
        public string Geo { get; set; }

        public ContactInfo ContactInfo { get; set; }

        [JsonProperty("Sitelinks")]
        public List<SiteLinkInfo> SiteLinks { get; set; }

        public List<string> MinusKeywords { get; set; }
    }
}
