using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Direct
{
    partial class YandexDirectServiceStub
    {
        public int CreateNewForecast(string[] phrases, int[] geoIds = null, int[] categoryIds = null)
        {
            if (phrases == null || phrases.Length == 0)
                throw new ArgumentNullException("phrases");

            return 1;
        }

        public ForecastInfo GetForecast(int forecastId)
        {
            return new ForecastInfo();
        }

        public List<ForecastStatus> GetForecastList()
        {
            return new List<ForecastStatus>();
        }

        public void DeleteForecastReport(int forecastReportId)
        {
        }
    }
}
