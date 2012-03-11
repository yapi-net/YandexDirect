using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Yandex.Direct.Serialization;

namespace Yandex.Direct
{
    [DebuggerDisplay("{Id}: {Name}")]
    public class ShortCampaignInfo
    {
        [JsonProperty("CampaignID")]
        public int Id { get; set; }

        public string Login { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }

        public decimal Sum { get; set; }
        public decimal Rest { get; set; }

        public decimal SumAvailableForTransfer { get; set; }

        public int Shows { get; set; }
        public int Clicks { get; set; }

        public string Status { get; set; }

        [JsonConverter(typeof(YesNoConverter))]
        public bool StatusShow { get; set; }

        [JsonConverter(typeof(YesNoConverter))]
        public bool StatusArchive { get; set; }

        public ActivatingStatus StatusActivating { get; set; }

        public ModerationStatus StatusModerate { get; set; }

        [JsonConverter(typeof(YesNoConverter))]
        public bool IsActive { get; set; }

        public string ManagerName { get; set; }
        public string AgencyName { get; set; }
    }
}