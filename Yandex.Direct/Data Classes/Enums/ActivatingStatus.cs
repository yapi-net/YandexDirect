using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Direct
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ActivatingStatus
    {
        Pending,
        Yes,
        No
    }
}
