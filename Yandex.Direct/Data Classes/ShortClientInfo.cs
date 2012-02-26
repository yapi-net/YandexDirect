using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Yandex.Direct.Serialization;

namespace Yandex.Direct
{
    [DebuggerDisplay("{Login}")]
    public class ShortClientInfo
    {
        [JsonProperty("Login")]
        public string Login { get; set; }

        [JsonProperty("DateCreate")]
        public DateTime CreationDate { get; set; }

        public string Email { get; set; }

        public decimal Discount { get; set; }

        [JsonProperty("StatusArch")]
        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool StatusArchive { get; set; }
    }
}