using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yandex.Direct
{
    public class NewReportFilterInfo
    {
        public PageFilterType? PageType { get; set; }

        public PositionFilterType? PositionType { get; set; }

        [JsonProperty("Banner")]
        public List<int> BannerIds { get; set; }

        [JsonProperty("Geo")]
        public List<int> GeoIds { get; set; }

        [JsonProperty("Phrase")]
        public List<string> Phrases { get; set; }

        [JsonProperty("PageName")]
        public List<string> PageNames { get; set; }

        [JsonProperty("StatGoals")]
        public List<int> StatGoals { get; set; }
    }
}