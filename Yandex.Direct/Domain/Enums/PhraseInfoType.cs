using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Direct
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PhraseInfoType
    {
        /// <summary>Do not return phrase parameters.</summary>
        No,

        /// <summary>Return abbreviated phrase information.</summary>
        Yes,

        /// <summary>Return complete phrase information, including prices and statistics.</summary>
        WithPrices
    }
}