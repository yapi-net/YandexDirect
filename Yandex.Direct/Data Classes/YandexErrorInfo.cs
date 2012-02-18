using Newtonsoft.Json;

namespace Yandex.Direct
{
    [JsonObject]
    public sealed class YandexErrorInfo
    {
        [JsonProperty("error_str")]
        public string Error { get; set; }

        [JsonProperty("error_detail")]
        public string Description { get; set; }

        [JsonProperty("error_code")]
        public int Code { get; set; }
    }
}