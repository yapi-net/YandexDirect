using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Direct
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PositionFilterType
    {
        [EnumMember( Value = "premium")]
        Premium,
        [EnumMember(Value = "other")]
        Other,
    }
}