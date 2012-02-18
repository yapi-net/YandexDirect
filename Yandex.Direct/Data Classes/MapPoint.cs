using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Yandex.Direct
{
    public class MapPoint
    {
        [JsonProperty("x")]
        public float X { get; set; }

        [JsonProperty("y")]
        public float Y { get; set; }

        [JsonProperty("x1")]
        public float X1 { get; set; }

        [JsonProperty("y1")]
        public float Y1 { get; set; }

        [JsonProperty("x2")]
        public float X2 { get; set; }

        [JsonProperty("y2")]
        public float Y2 { get; set; }
    }
}
