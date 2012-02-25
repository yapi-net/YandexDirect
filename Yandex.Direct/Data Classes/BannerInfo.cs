using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Yandex.Direct.Serialization;

namespace Yandex.Direct
{
    public class BannerInfo : BannerInfoBase
    {
        public string Domain { get; set; }

        public ActivatingStatus StatusActivating { get; set; }

        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool StatusArchive { get; set; }

        public ModerationStatus StatusBannerModerate { get; set; }
        public ModerationStatus StatusPhrasesModerate { get; set; }
        public ModerationStatus StatusPhoneModerate { get; set; }

        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool StatusShow { get; set; }

        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool IsActive { get; set; }

        public ModerationStatus? StatusSitelinksModerate { get; set; }

        //TODO: Convert to Enum Flags (Issue #4)
        public List<string> AdWarnings { get; set; }

        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool FixedOnModeration { get; set; }

        public List<RejectReason> ModerateRejectionReasons { get; set; }
    }
}