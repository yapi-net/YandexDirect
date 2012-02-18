using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Direct
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BooleanType
    {
        [EnumMember(Value = "No")]
        No,
        [EnumMember(Value = "Yes")]
        Yes,
    }
}