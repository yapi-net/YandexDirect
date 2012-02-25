using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Yandex.Direct.Serialization;

namespace Yandex.Direct
{
    public class BannersFilterInfo
    {
        public IList<ModerationStatus> StatusPhoneModerate { get; set; }

        public IList<ModerationStatus> StatusPhrasesModerate { get; set; }

        public IList<ModerationStatus> StatusBannerModerate { get; set; }

        public IList<ActivatingStatus> StatusActivating { get; set; }

        [JsonConverter(typeof(YesNoArrayConverter))]
        public bool? StatusShow { get; set; }

        [JsonConverter(typeof(YesNoArrayConverter))]
        public bool? IsActive { get; set; }

        [JsonConverter(typeof(YesNoArrayConverter))]
        public bool? StatusArchive { get; set; }
    }
}
