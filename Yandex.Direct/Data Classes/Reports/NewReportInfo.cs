using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yandex.Direct
{
    [JsonObject]
    public class NewReportInfo
    {
        [JsonProperty("CampaignID")]
        public int CampaignId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<ReportColumn> GroupByColumns { get; set; }

        public int? Limit { get; set; }
        public int? Offset { get; set; }

        public DateGroupingType GroupByDate { get; set; }

        public List<ReportColumn> OrderBy { get; set; }

        [JsonProperty("TypeResultReport")]
        public ReportType ReportType { get; set; }

        public ReportCompressType? CompressReport { get; set; }

        public NewReportFilterInfo Filter { get; set; }
    }
}