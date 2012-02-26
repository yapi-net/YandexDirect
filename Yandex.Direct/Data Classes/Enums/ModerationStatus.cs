using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Direct
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ModerationStatus
    {
        New,
        Pending,
        Yes,
        No,
        PreliminaryAccept
    }
}
