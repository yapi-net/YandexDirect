using System.Diagnostics;
using Newtonsoft.Json;

namespace Yandex.Direct
{
    [JsonObject, DebuggerDisplay("{Id} - {Sum}")]
    public class TransferInfo
    {
        [JsonProperty("CampaignID")]
        public long CampaignId { get; set; }

        [JsonProperty("Sum")]
        public decimal Sum { get; set; }
    }
}