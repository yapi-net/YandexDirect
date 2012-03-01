using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Yandex.Direct.Serialization;

namespace Yandex.Direct
{
    public class BannerPhraseInfoWithStats : BannerPhraseInfo
    {
        public int Clicks { get; set; }
        public int Shows { get; set; }

        public decimal Min { get; set; }
        public decimal Max { get; set; }

        public decimal PremiumMin { get; set; }
        public decimal PremiumMax { get; set; }

        public List<decimal> Prices { get; set; }
        public decimal CurrentOnSearch { get; set; }
        public decimal MinPrice { get; set; }

        [JsonProperty("LowCTRWarning")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool IsLowCtrWarning { get; set; }

        [JsonProperty("LowCTR")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool IsLowCtr { get; set; }

        [JsonProperty("ContextLowCTR")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool IsContextLowCtr { get; set; }

        public List<PhraseCoverageInfo> Coverage { get; set; }
        public List<PhraseCoverageInfo> ContextCoverage { get; set; }
    }
}
