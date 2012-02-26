using Newtonsoft.Json;

namespace Yandex.Direct
{
    public class ReportInfo
    {
        [JsonProperty("ReportID")]
        public int ReportId { get; set; }

        public string Url { get; set; }

        [JsonProperty("StatusReport")]
        public ReportStatus Status { get; set; }
    }
}