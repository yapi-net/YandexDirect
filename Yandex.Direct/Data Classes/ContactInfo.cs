using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Yandex.Direct
{
    public class ContactInfo
    {
        public string ContactPerson { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Build { get; set; }
        public string Apart { get; set; }
        public string CityCode { get; set; }
        public string Phone { get; set; }
        public string PhoneExt { get; set; }
        public string CompanyName { get; set; }
        public string IMClient { get; set; }
        public string IMLogin { get; set; }
        public string ExtraMessage { get; set; }
        public string ContactEmail { get; set; }
        public string WorkTime { get; set; }

        [JsonProperty("OGRN")]
        public string Ogrn { get; set; }

        public MapPoint PointOnMap { get; set; }
    }
}
