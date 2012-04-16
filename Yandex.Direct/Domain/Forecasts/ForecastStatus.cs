using Newtonsoft.Json;

namespace Yandex.Direct
{
    /// <summary>
    /// Состояние прогноза
    /// </summary>
    public class ForecastStatus
    {
        [JsonProperty("ForecastID")]
        public int ForecastId { get; set; }

        [JsonProperty("StatusForecast")]
        public ReportStatus Status { get; set; }
    }
}