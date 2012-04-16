using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Yandex.Direct.Data_Classes.Enums;

namespace Yandex.Direct
{
    [DebuggerDisplay("{CompanyName}, {City} {Street} {House}")]
    public class ContactInfo
    {
        public string ContactPerson { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }

        [JsonProperty("Build")]
        public string Building { get; set; }

        [JsonProperty("Apart")]
        public string Apartment { get; set; }

        public string CountryCode { get; set; }
        public string CityCode { get; set; }
        public string Phone { get; set; }
        public string PhoneExt { get; set; }

        public string CompanyName { get; set; }

        [JsonProperty("IMClient")]
        public InstantMessagingClient ImClient { get; set; }

        [JsonProperty("IMLogin")]
        public string ImLogin { get; set; }
        
        public string ExtraMessage { get; set; }
        public string ContactEmail { get; set; }

        //TODO: Convert to specialized structure (issue #3)
        public string WorkTime { get; set; }

        [JsonProperty("OGRN")]
        public string Ogrn { get; set; }

        public MapPoint PointOnMap { get; set; }
    }
}
