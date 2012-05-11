using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Direct
{
    partial class YandexDirectServiceStub
    {
        public int CreateNewReport(NewReportInfo reportInfo)
        {
            if (reportInfo == null)
                throw new ArgumentNullException("reportInfo");

            if (reportInfo.Limit != null && reportInfo.Offset != null)
                throw new ArgumentException("Only one of \"Limit\" and \"Offset\" should be set");

            return 1;
        }

        public List<ReportInfo> GetReportList()
        {
            return new List<ReportInfo>();
        }

        public List<GoalInfo> GetStatGoals(int campaignId)
        {
            return new List<GoalInfo>();
        }

        public void DeleteReport(int reportId)
        {
        }
    }
}
