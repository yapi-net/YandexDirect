using Newtonsoft.Json;

namespace Yandex.Direct
{
    public class YapiResponse<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}