using System.Diagnostics;
using Newtonsoft.Json;

namespace Yandex.Direct
{
    [JsonObject, DebuggerDisplay("{Login} - {UnitsRest} unit(s)")]
    public class ClientUnitInfo
    {
        /// <summary>Client's login</summary>
        public string Login { get; set; }

        /// <summary>Amount of units availiable to spend</summary>
        public int UnitsRest { get; set; }
    }
}