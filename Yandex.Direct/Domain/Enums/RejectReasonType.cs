using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Direct
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RejectReasonType
    {
        Banner,
        Phrases,
        ContactInfo,
        Sitelink
    }
}
