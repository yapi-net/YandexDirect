using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Direct
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReportStatus
    {
        [EnumMember(Value = "Done")]
        Done,
        [EnumMember(Value = "Pending")]
        Pending,
    }
}