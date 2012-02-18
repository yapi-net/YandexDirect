using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Yandex.Direct.Serialization;

namespace Yandex.Direct
{
    [DebuggerDisplay("{PhraseId}: {Phrase}")]
    public class BannerPhraseInfo
    {
        [JsonProperty("BannerID")]
        public int BannerId { get; set; }

        [JsonProperty("CampaignID")]
        public int CampaignId { get; set; }

        [JsonProperty("PhraseID")]
        public int PhraseId { get; set; }

        public string Phrase { get; set; }

        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool? IsRubric { get; set; }

        public decimal? Price { get; set; }
        public decimal? ContextPrice { get; set; }

        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool? AutoBroker { get; set; }

        public PhraseUserParams UserParams { get; set; }

        public ModerationStatus StatusPhraseModerate { get; set; }

        public AutoBudgetPriority? AutoBudgetPriority { get; set; }

        public int Clicks { get; set; }
        public int Shows { get; set; }

        public decimal? Min { get; set; }
        public decimal? Max { get; set; }

        public decimal? PremiumMin { get; set; }
        public decimal? PremiumMax { get; set; }

        [JsonProperty("LowCTRWarning")]
        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool? IsLowCtrWarning { get; set; }

        [JsonProperty("LowCTR")]
        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool? IsLowCtr { get; set; }

        [JsonProperty("ContextLowCTR")]
        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool? IsContextLowCtr { get; set; }

        public List<PhraseCoverageInfo> Coverage { get; set; }
        public List<PhraseCoverageInfo> ContextCoverage { get; set; }

        public List<decimal> Prices { get; set; }
        public decimal? CurrentOnSearch { get; set; }
        public decimal? MinPrice { get; set; }
    }
}