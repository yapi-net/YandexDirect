using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yandex.Direct
{
    public class BannerPhraseInfo : BannerPhraseInfoBase
    {
        [JsonProperty("BannerID")]
        public int BannerId { get; set; }

        [JsonProperty("CampaignID")]
        public int CampaignId { get; set; }

        public ModerationStatus StatusPhraseModerate { get; set; }
    }
}