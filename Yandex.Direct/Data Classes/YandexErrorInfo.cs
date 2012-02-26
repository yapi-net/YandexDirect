using Newtonsoft.Json;

namespace Yandex.Direct
{
    public sealed class YandexErrorInfo
    {
        [JsonProperty("error_str")]
        public string Error { get; set; }

        [JsonProperty("error_detail")]
        public string Description { get; set; }

        [JsonProperty("error_code")]
        public YapiService.YapiErrorCode Code { get; set; }
    }
}