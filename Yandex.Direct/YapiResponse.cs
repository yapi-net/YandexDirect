using Newtonsoft.Json;

namespace Yandex.Direct
{
    [JsonObject]
    public class YapiResponse<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}