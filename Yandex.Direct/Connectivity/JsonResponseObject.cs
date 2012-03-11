using Newtonsoft.Json;

namespace Yandex.Direct.Connectivity
{
    public class JsonResponseObject<T>
    {
        [JsonProperty("error_code")]
        public YandexApiErrorCode ErrorCode { get; set; }

        [JsonProperty("error_str")]
        public string ErrorMessage { get; set; }

        [JsonProperty("error_detail")]
        public string ErrorDescription { get; set; }

        [JsonProperty("data")]
        public T Object { get; set; }
    }
}