using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using Yandex.Direct.Authentication;
using Yandex.Direct.Configuration;
using Yandex.Direct.Connectivity;
using Yandex.Direct.Serialization;

namespace Yandex.Direct
{
    public partial class YapiService
    {
        protected IYandexApiClient YandexApiClient { get; set; }

        public YapiService(YapiSettings settings)
        {
            Contract.Requires(settings != null);

            IYandexAuthProvider authProvider;

            switch (settings.AuthType)
            {
                case YapiAuthType.Token:
                    authProvider = new TokenAuthProvider();
                    break;

                case YapiAuthType.Certificate:
                    authProvider = new CertificateFileAuthProvider();
                    break;

                default:
                    authProvider = null;
                    break;
            }

            YandexApiClient = new JsonYandexApiClient(settings, authProvider);

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, errors) => true;
        }

        /// <summary>
        /// Создать клиента для Яндекс-Директа, используя файл конфигурации (секция yandex.direct)
        /// </summary>
        public YapiService()
            : this(YapiSettings.FromConfiguration())
        {
        }

        /// <summary>
        /// Создать клиента для Яндекс-Директа, указав путь к файлу сертификата и пароль
        /// </summary>
        public YapiService(string certificatePath, string certificatePassword)
            : this(new YapiSettings(certificatePath, certificatePassword))
        {
        }

        #region PingApi

        public int PingApi()
        {
            return YandexApiClient.Invoke<int>(ApiCommand.PingAPI.ToString());
        }

        public void TestApiConnection()
        {
            if (PingApi() != 1)
                throw new YapiServerException("Connection ping attempt failed.");
        }

        #endregion

        #region Clients

        public List<ShortClientInfo> GetClientLogins()
        {
            return YandexApiClient.Invoke<List<ShortClientInfo>>(ApiCommand.GetClientsList.ToString());
        }

        public ClientUnitInfo[] GetClientsUnits(params string[] logins)
        {
            Contract.Requires<ArgumentException>(logins != null && logins.Any(), "logins");
            return YandexApiClient.Invoke<ClientUnitInfo[]>(ApiCommand.GetClientsUnits.ToString(), logins);
        }

        #endregion

        #region Campaigns

        /*
         * Not implemented:
           - CreateOrUpdateCampaign
           - GetBalance
           - GetCampaignParams
           - GetCampaignsList
           - GetCampaignsListFilter
           - GetCampaignsParams
         */

        public List<ShortCampaignInfo> GetClientCampaigns(params string[] logins)
        {
            Contract.Requires<ArgumentException>(logins != null && logins.Any(), "logins");
            return YandexApiClient.Invoke<List<ShortCampaignInfo>>(ApiCommand.GetCampaignsList.ToString(), logins);
        }

        #endregion

        #region TransferMoney

        public void TransferMoney(TransferInfo from, TransferInfo to)
        {
            TransferMoney(new[] { from }, new[] { to });
        }

        public void TransferMoney(TransferInfo[] from, TransferInfo[] to)
        {
            var request = new { FromCampaigns = from, ToCampaigns = to };
            YandexApiClient.Invoke<int>(ApiCommand.TransferMoney.ToString(), request, true);
        }

        #endregion

        #region Ads and phrases

        public BannerInfo GetBanner(int bannerId)
        {
            return GetBanners(new[] { bannerId }).FirstOrDefault();
        }

        public BannerInfoWithPhrases<BannerPhraseInfo> GetBannerWithPhrases(BannerInfo banner)
        {
            Contract.Requires<ArgumentNullException>(banner != null, "banner");

            return GetBannerWithPhrases(banner.BannerId);
        }

        public BannerInfoWithPhrases<BannerPhraseInfo> GetBannerWithPhrases(int bannerId)
        {
            return GetBannersWithPhrases(new[] { bannerId }).FirstOrDefault();
        }

        public BannerInfoWithPhrases<BannerPhraseInfoWithStats> GetBannerWithPhrasesAndStats(BannerInfo banner)
        {
            Contract.Requires<ArgumentNullException>(banner != null, "banner");

            return GetBannerWithPhrasesAndStats(banner.BannerId);
        }

        public BannerInfoWithPhrases<BannerPhraseInfoWithStats> GetBannerWithPhrasesAndStats(int bannerId)
        {
            return GetBannersWithPhrasesAndStats(new[] { bannerId }).FirstOrDefault();
        }

        public List<BannerInfo> GetBanners(int[] bannerIds, BannersFilterInfo filter = null)
        {
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");
            Contract.Requires<ArgumentOutOfRangeException>(bannerIds.Length <= 2000, "Maximum allowed number of identifiers per call is 2000.");

            return GetBannersInternal<BannerInfo>(null, bannerIds, PhraseInfoType.No, filter);
        }

        public List<BannerInfoWithPhrases<BannerPhraseInfo>> GetBannersWithPhrases(int[] bannerIds, BannersFilterInfo filter = null)
        {
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");
            Contract.Requires<ArgumentOutOfRangeException>(bannerIds.Length <= 2000, "Maximum allowed number of identifiers per call is 2000.");

            return GetBannersInternal<BannerInfoWithPhrases<BannerPhraseInfo>>(null, bannerIds, PhraseInfoType.Yes, filter);
        }

        public List<BannerInfoWithPhrases<BannerPhraseInfoWithStats>> GetBannersWithPhrasesAndStats(int[] bannerIds, BannersFilterInfo filter = null)
        {
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");
            Contract.Requires<ArgumentOutOfRangeException>(bannerIds.Length <= 2000, "Maximum allowed number of identifiers per call is 2000.");

            return GetBannersInternal<BannerInfoWithPhrases<BannerPhraseInfoWithStats>>(null, bannerIds, PhraseInfoType.WithPrices, filter);
        }

        public List<BannerInfo> GetBannersForCampaign(int campaignId, BannersFilterInfo filter = null)
        {
            return GetBannersInternal<BannerInfo>(new[] { campaignId }, null, PhraseInfoType.No, filter);
        }

        public List<BannerInfoWithPhrases<BannerPhraseInfo>> GetBannersForCampaignWithPhrases(int campaignId, BannersFilterInfo filter = null)
        {
            return GetBannersInternal<BannerInfoWithPhrases<BannerPhraseInfo>>(new[] { campaignId }, null, PhraseInfoType.Yes, filter);
        }

        public List<BannerInfoWithPhrases<BannerPhraseInfoWithStats>> GetBannersForCampaignWithPhrasesAndStats(int campaignId, BannersFilterInfo filter = null)
        {
            return GetBannersInternal<BannerInfoWithPhrases<BannerPhraseInfoWithStats>>(new[] { campaignId }, null, PhraseInfoType.WithPrices, filter);
        }

        private List<T> GetBannersInternal<T>(int[] campaingIds, int[] bannerIds, PhraseInfoType phraseDetails, BannersFilterInfo filter)
            where T : BannerInfo
        {
            var request = new { CampaignIDS = campaingIds, BannerIDS = bannerIds, GetPhrases = phraseDetails, Filter = filter };

            return YandexApiClient.Invoke<List<T>>(ApiCommand.GetBanners.ToString(), request);
        }

        public List<BannerPhraseInfo> GetBannerPhrases(BannerInfo banner, bool considerTimeTarget = false)
        {
            Contract.Requires<ArgumentNullException>(banner != null, "banner");

            return GetBannerPhrases(banner.BannerId, considerTimeTarget);
        }

        public List<BannerPhraseInfoWithStats> GetBannerPhrasesWithStats(BannerInfo banner, bool considerTimeTarget = false)
        {
            Contract.Requires<ArgumentNullException>(banner != null, "banner");

            return GetBannerPhrasesWithStats(banner.BannerId, considerTimeTarget);
        }

        public List<BannerPhraseInfo> GetBannerPhrases(int bannerId, bool considerTimeTarget = false)
        {
            return GetBannerPhrases(new[] { bannerId }, considerTimeTarget);
        }

        public List<BannerPhraseInfoWithStats> GetBannerPhrasesWithStats(int bannerId, bool considerTimeTarget = false)
        {
            return GetBannerPhrasesWithStats(new[] { bannerId }, considerTimeTarget);
        }

        public List<BannerPhraseInfo> GetBannerPhrases(int[] bannerIds, bool considerTimeTarget = false)
        {
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");
            Contract.Requires<ArgumentOutOfRangeException>(bannerIds.Length <= 1000, "Maximum allowed number of identifiers per call is 1000.");

            var request = new { BannerIDS = bannerIds, RequestPrices = YesNo.No, ConsiderTimeTarget = (YesNo)considerTimeTarget };

            return YandexApiClient.Invoke<List<BannerPhraseInfo>>(ApiCommand.GetBannerPhrasesFilter.ToString(), request);
        }

        public List<BannerPhraseInfoWithStats> GetBannerPhrasesWithStats(int[] bannerIds, bool considerTimeTarget = false)
        {
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");
            Contract.Requires<ArgumentOutOfRangeException>(bannerIds.Length <= 1000, "Maximum allowed number of identifiers per call is 1000.");

            var request = new { BannerIDS = bannerIds, RequestPrices = YesNo.Yes, ConsiderTimeTarget = (YesNo)considerTimeTarget };

            return YandexApiClient.Invoke<List<BannerPhraseInfoWithStats>>(ApiCommand.GetBannerPhrasesFilter.ToString(), request);
        }

        public int CreateOrUpdateBanner(EditableBannerInfo banner)
        {
            Contract.Requires<ArgumentNullException>(banner != null, "banner");

            return CreateOrUpdateBanners(new[] { banner }).FirstOrDefault();
        }

        public List<int> CreateOrUpdateBanners(IEnumerable<EditableBannerInfo> banners)
        {
            Contract.Requires<ArgumentNullException>(banners != null, "banner");

            var bannersArray = banners.ToArray();

            if (bannersArray.Contains(null))
                throw new ArgumentNullException("banners", "One of the items is null.");

            return YandexApiClient.Invoke<List<int>>(ApiCommand.CreateOrUpdateBanners.ToString(), bannersArray);
        }

        #endregion

        #region Ad status

        public bool ArchiveBanners(int campaignId, int[] bannerIds)
        {
            Contract.Requires<ArgumentException>(campaignId > 0);
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");

            var request = new { CampaignID = campaignId, BannerIDS = bannerIds };

            return YandexApiClient.Invoke<int>(ApiCommand.ArchiveBanners.ToString(), request) == 1;
        }

        public bool DeleteBanners(int campaignId, int[] bannerIds)
        {
            Contract.Requires<ArgumentException>(campaignId > 0);
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");

            var request = new { CampaignID = campaignId, BannerIDS = bannerIds };

            return YandexApiClient.Invoke<int>(ApiCommand.DeleteBanners.ToString(), request) == 1;
        }

        public bool ModerateBanners(int campaignId, int[] bannerIds)
        {
            Contract.Requires<ArgumentException>(campaignId > 0);
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");

            var request = new { CampaignID = campaignId, BannerIDS = bannerIds };

            return YandexApiClient.Invoke<int>(ApiCommand.ModerateBanners.ToString(), request) == 1;
        }

        public bool ResumeBanners(int campaignId, int[] bannerIds)
        {
            Contract.Requires<ArgumentException>(campaignId > 0);
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");

            var request = new { CampaignID = campaignId, BannerIDS = bannerIds };

            return YandexApiClient.Invoke<int>(ApiCommand.ResumeBanners.ToString(), request) == 1;
        }

        public bool StopBanners(int campaignId, int[] bannerIds)
        {
            Contract.Requires<ArgumentException>(campaignId > 0);
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");

            var request = new { CampaignID = campaignId, BannerIDS = bannerIds };

            return YandexApiClient.Invoke<int>(ApiCommand.StopBanners.ToString(), request) == 1;
        }

        public bool UnArchiveBanners(int campaignId, int[] bannerIds)
        {
            Contract.Requires<ArgumentException>(campaignId > 0);
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");

            var request = new { CampaignID = campaignId, BannerIDS = bannerIds };

            return YandexApiClient.Invoke<int>(ApiCommand.UnArchiveBanners.ToString(), request) == 1;
        }

        #endregion

        #region Reporting

        public int CreateReport(NewReportInfo reportInfo)
        {
            Contract.Requires<ArgumentNullException>(reportInfo != null, "reportInfo");
            Contract.Requires<YapiException>(reportInfo.Limit.HasValue ^ reportInfo.Offset.HasValue, "Only one of \"Limit\" and \"Offset\" should be set");

            return YandexApiClient.Invoke<int>(ApiCommand.CreateNewReport.ToString(), reportInfo);
        }

        public List<ReportInfo> ListReports()
        {
            return YandexApiClient.Invoke<List<ReportInfo>>(ApiCommand.GetReportList.ToString());
        }

        public List<GoalInfo> GetStatGoals(int campaignId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(campaignId > 0, "campaignId");

            return YandexApiClient.Invoke<List<GoalInfo>>(ApiCommand.GetStatGoals.ToString(), new { CampaignID = campaignId });
        }

        public void DeleteReport(int reportId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(reportId > 0, "reportId");
            var result = YandexApiClient.Invoke<int>(ApiCommand.DeleteReport.ToString(), reportId);
            if (result != 1)
                throw new YapiServerException(string.Format("Плохой ответ. Должен вернуть: 1. Вернул: {0}", result));
        }

        #endregion

        #region Forecasts

        public int CreateForecast(string[] phrases, int[] geoIds = null, int[] categoryIds = null)
        {
            Contract.Requires<ArgumentException>(phrases != null && phrases.Any(), "phrases");
            
            var request = new { Categories = categoryIds, GeoID = geoIds, Phrases = phrases };

            return YandexApiClient.Invoke<int>(ApiCommand.CreateNewForecast.ToString(), request);
        }

        public ForecastInfo GetForecast(int forecastId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(forecastId > 0, "forecastId");
            return YandexApiClient.Invoke<ForecastInfo>(ApiCommand.GetForecast.ToString(), forecastId);
        }

        public List<ForecastStatus> ListForecasts()
        {
            return YandexApiClient.Invoke<List<ForecastStatus>>(ApiCommand.GetForecastList.ToString());
        }

        public void DeleteForecast(int forecastReportId)
        {
            var result = YandexApiClient.Invoke<int>(ApiCommand.DeleteForecastReport.ToString(), forecastReportId);
            if (result != 1)
                throw new YapiServerException(string.Format("Плохой ответ. Должен вернуть: 1. Вернул: {0}", result));
        }

        #endregion
    }
}
