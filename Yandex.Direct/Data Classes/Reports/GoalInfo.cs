using System.Diagnostics;
using Newtonsoft.Json;

namespace Yandex.Direct
{
    [DebuggerDisplay("{Id} - {Name}")]
    public class GoalInfo
    {
        /// <summary>Id of goal in Yandex.Direct</summary>
        [JsonProperty("GoalID")]
        public string Id { get; set; }

        /// <summary>Goal name</summary>
        public string Name { get; set; }
    }
}