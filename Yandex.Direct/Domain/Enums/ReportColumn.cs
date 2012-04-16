using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Direct
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReportColumn
    {
        [EnumMember(Value = "clBanner")]
        Banner,

        [EnumMember(Value = "clDate")]
        Date,

        [EnumMember(Value = "clPage")]
        Page,

        [EnumMember(Value = "clGeo")]
        Geo,

        [EnumMember(Value = "clPhrase")]
        Phrase,

        [EnumMember(Value = "clStatGoals")]
        StatGoals,

        [EnumMember(Value = "clPositionType")]
        PositionType,
    }
}