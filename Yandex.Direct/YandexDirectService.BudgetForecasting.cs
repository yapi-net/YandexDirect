using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Direct
{
    partial class YandexDirectService
    {
        public int CreateNewForecast(string[] phrases, int[] geoIds = null, int[] categoryIds = null)
        {
            if (phrases == null || phrases.Length == 0)
                throw new ArgumentNullException("phrases");

            var request = new { Categories = categoryIds, GeoID = geoIds, Phrases = phrases };

            return YandexApiClient.Invoke<int>(ApiMethod.CreateNewForecast, request);
        }

        public ForecastInfo GetForecast(int forecastId)
        {
            return YandexApiClient.Invoke<ForecastInfo>(ApiMethod.GetForecast, forecastId);
        }

        public List<ForecastStatus> GetForecastList()
        {
            return YandexApiClient.Invoke<List<ForecastStatus>>(ApiMethod.GetForecastList);
        }

        public void DeleteForecastReport(int forecastReportId)
        {
            var result = YandexApiClient.Invoke<int>(ApiMethod.DeleteForecastReport, forecastReportId);

            if (result != 1)
                throw new YandexDirectException("Method DeleteForecastReport failed.");
        }
    }
}
