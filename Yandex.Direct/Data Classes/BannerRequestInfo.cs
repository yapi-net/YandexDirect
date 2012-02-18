using System;
using Newtonsoft.Json;

namespace Yandex.Direct
{
    [JsonObject]
    internal class BannerRequestInfo
    {
        [JsonProperty("CampaignIDS")]
        public int[] CampaignId { get; set; }

        [JsonProperty("BannerIDS")]
        public int[] BannerIds { get; set; }

        [JsonProperty("GetPhrases")]
        public PhraseInfoType GetPhrases { get; set; }
    }
}