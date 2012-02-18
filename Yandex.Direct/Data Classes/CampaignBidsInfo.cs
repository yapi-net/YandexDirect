using Newtonsoft.Json;

namespace Yandex.Direct
{
    [JsonObject]
    internal class CampaignBidsInfo 
    {
        [JsonProperty("CampaignID")]
        public int CampaignId { get; set; }

        [JsonProperty("BannerIDS")]
        public int[] BannerIds { get; set; }
    }
}