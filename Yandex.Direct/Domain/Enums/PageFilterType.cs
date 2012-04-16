using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Direct
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PageFilterType
    {
        [EnumMember(Value = "search")]
        Search,

        [EnumMember(Value = "context")]
        Context,

        [EnumMember(Value = "all")]
        All,
    }
}