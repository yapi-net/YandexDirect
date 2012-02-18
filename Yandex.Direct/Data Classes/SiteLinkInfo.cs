using Newtonsoft.Json;

namespace Yandex.Direct
{
    [JsonObject]
    public class SiteLinkInfo
    {
        public string Title { get; set; }

        public string Href { get; set; }
    }
}