using Newtonsoft.Json;

namespace Yandex.Direct
{
    /// <summary>
    /// Internal class for calling GetStatGoals
    /// For inner use only
    /// </summary>
    [JsonObject]
    internal class StatGoalRequestInfo
    {
        public StatGoalRequestInfo(int campId)
        {
            this.CampaignId = campId;
        }

        [JsonProperty("CampaignID")]
        public int CampaignId { get; set; }
    }
}