using System.Diagnostics;
using Newtonsoft.Json;

namespace Yandex.Direct
{
    [JsonObject, DebuggerDisplay("{Login}")]
    public class ShortClientInfo
    {
        [JsonProperty("Login")]
        public string Login { get; set; }
    }
}