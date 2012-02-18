using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Yandex.Direct.Serialization;

namespace Yandex.Direct
{
    [DebuggerDisplay("{BannerId}: {Title}")]
    public class BannerInfo
    {
        [JsonProperty("BannerID")]
        public int BannerId { get; set; }

        [JsonProperty("CampaignID")]
        public int CampaignId { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        public string Href { get; set; }
        public string Domain { get; set; }

        //TODO: Convert to list of regions (or at leaset of region ids)
        public string Geo { get; set; }

        public ContactInfo ContactInfo { get; set; }

        public List<BannerPhraseInfo> Phrases { get; set; }

        public ActivatingStatus StatusActivating { get; set; }

        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool StatusArchive { get; set; }

        public ModerationStatus StatusBannerModerate { get; set; }
        public ModerationStatus StatusPhrasesModerate { get; set; }
        public ModerationStatus StatusPhoneModerate { get; set; }

        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool StatusShow { get; set; }

        //WHY? [Obsolete("Bad way")]
        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool IsActive { get; set; }

        public ModerationStatus? StatusSitelinksModerate { get; set; }

        [JsonProperty("Sitelinks")]
        public List<SiteLinkInfo> SiteLinks { get; set; }

        //TODO: Convert to Enum Flags
        public List<string> AdWarnings { get; set; }

        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool FixedOnModeration { get; set; }

        public List<RejectReason> ModerateRejectionReasons { get; set; }

        public List<string> MinusKeywords { get; set; }
    }
}