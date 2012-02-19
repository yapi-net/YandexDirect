using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Yandex.Direct.Serialization;

namespace Yandex.Direct
{
    public partial class YapiService
    {
        #region Constructors and Properties

        public YapiSettings Setting { get; private set; }
        private YapiTransport YapiTransport { get; set; }

        public YapiService(YapiSettings settings)
        {
            Contract.Requires(settings != null);
            this.Setting = settings;
            this.YapiTransport = new YapiTransport(settings);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, errors) => true;
        }

        /// <summary>
        /// Создать клиента для Яндекс-Директа, используя файл конфигурации (секция yandex.direct)
        /// </summary>
        public YapiService()
            : this(YapiSettings.FromConfiguration())
        { }

        /// <summary>
        /// Создать клиента для Яндекс-Директа, указав путь к файлу сертификата и пароль
        /// </summary>
        public YapiService(string certificatePath, string certificatePassword)
            : this(new YapiSettings(certificatePath, certificatePassword))
        { }

        #endregion


        #region PingApi

        public int PingApi()
        {
            return YapiTransport.Request<int>(ApiCommand.PingAPI);
        }

        public void TestApiConnection()
        {
            if (PingApi() != 1)
                throw new YapiServerException("Test API failed");
        }

        #endregion

        #region Clients

        public List<ShortClientInfo> GetClientLogins()
        {
            return YapiTransport.Request<List<ShortClientInfo>>(ApiCommand.GetClientsList);
        }

        public ClientUnitInfo[] GetClientsUnits(params string[] logins)
        {
            Contract.Requires<ArgumentException>(logins != null && logins.Any(), "logins");
            return YapiTransport.Request<ClientUnitInfo[]>(ApiCommand.GetClientsUnits, logins);
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
            return YapiTransport.Request<List<ShortCampaignInfo>>(ApiCommand.GetCampaignsList, logins);
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
            YapiTransport.Request<int>(ApiCommand.TransferMoney, request, true);
        }

        #endregion

        #region Ads and phrases

        public BannerInfo GetBanner(int bannerId)
        {
            return GetBanners(new[] { bannerId }).FirstOrDefault();
        }

        public BannerInfoWithPhrases<BannerPhraseInfo> GetBannerWithPhrases(BannerInfo banner)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

            return GetBannerWithPhrases(banner.BannerId);
        }

        public BannerInfoWithPhrases<BannerPhraseInfo> GetBannerWithPhrases(int bannerId)
        {
            return GetBannersWithPhrases(new[] { bannerId }).FirstOrDefault();
        }

        public BannerInfoWithPhrases<BannerPhraseInfoWithStats> GetBannerWithPhrasesAndStats(BannerInfo banner)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

            return GetBannerWithPhrasesAndStats(banner.BannerId);
        }

        public BannerInfoWithPhrases<BannerPhraseInfoWithStats> GetBannerWithPhrasesAndStats(int bannerId)
        {
            return GetBannersWithPhrasesAndStats(new[] { bannerId }).FirstOrDefault();
        }

        public List<BannerInfo> GetBanners(int[] bannerIds, BannersFilterInfo filter = null)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            if (bannerIds.Length > 2000)
                throw new ArgumentOutOfRangeException("bannerIds", "Maximum allowed number of identifiers per call is 2000.");

            return GetBannersInternal<BannerInfo>(null, bannerIds, PhraseInfoType.No, filter);
        }

        public List<BannerInfoWithPhrases<BannerPhraseInfo>> GetBannersWithPhrases(int[] bannerIds, BannersFilterInfo filter = null)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            if (bannerIds.Length > 2000)
                throw new ArgumentOutOfRangeException("bannerIds", "Maximum allowed number of identifiers per call is 2000.");

            return GetBannersInternal<BannerInfoWithPhrases<BannerPhraseInfo>>(null, bannerIds, PhraseInfoType.Yes, filter);
        }

        public List<BannerInfoWithPhrases<BannerPhraseInfoWithStats>> GetBannersWithPhrasesAndStats(int[] bannerIds, BannersFilterInfo filter = null)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            if (bannerIds.Length > 2000)
                throw new ArgumentOutOfRangeException("bannerIds", "Maximum allowed number of identifiers per call is 2000.");

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

            return Request<List<T>>(ApiCommand.GetBanners, request);
        }

        public List<BannerPhraseInfo> GetBannerPhrases(BannerInfo banner, bool considerTimeTarget = false)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

            return GetBannerPhrases(banner.BannerId, considerTimeTarget);
        }

        public List<BannerPhraseInfoWithStats> GetBannerPhrasesWithStats(BannerInfo banner, bool considerTimeTarget = false)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

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
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            if (bannerIds.Length > 1000)
                throw new ArgumentOutOfRangeException("bannerIds", "Maximum allowed number of identifiers per call is 1000.");

            var request = new { BannerIDS = bannerIds, RequestPrices = YesNo.No, ConsiderTimeTarget = (YesNo)considerTimeTarget };

            return Request<List<BannerPhraseInfo>>(ApiCommand.GetBannerPhrasesFilter, request);
        }

        public List<BannerPhraseInfoWithStats> GetBannerPhrasesWithStats(int[] bannerIds, bool considerTimeTarget = false)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            if (bannerIds.Length > 1000)
                throw new ArgumentOutOfRangeException("bannerIds", "Maximum allowed number of identifiers per call is 1000.");

            var request = new { BannerIDS = bannerIds, RequestPrices = YesNo.Yes, ConsiderTimeTarget = (YesNo)considerTimeTarget };

            return Request<List<BannerPhraseInfoWithStats>>(ApiCommand.GetBannerPhrasesFilter, request);
        }

        public int CreateOrUpdateBanner(EditableBannerInfo banner)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

            return CreateOrUpdateBanners(new[] { banner }).FirstOrDefault();
        }

        public List<int> CreateOrUpdateBanners(IEnumerable<EditableBannerInfo> banners)
        {
            if (banners == null)
                throw new ArgumentNullException("banners");

            var bannersArray = banners.ToArray();

            if (bannersArray.Contains(null))
                throw new ArgumentNullException("banners", "One of the items is null.");

            return Request<List<int>>(ApiCommand.CreateOrUpdateBanners, bannersArray);
        }

        #endregion

        #region Ad status

        public bool ArchiveBanners(int campaignId, int[] bannerIds)
        {
            Contract.Requires<ArgumentException>(campaignId > 0);
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");

            return YapiTransport.Request<int>(ApiCommand.ArchiveBanners, new CampaignBidsInfo { CampaignId = campaignId, BannerIds = bannerIds }) == 1;
        }

        public bool DeleteBanners(int campaignId, int[] bannerIds)
        {
            Contract.Requires<ArgumentException>(campaignId > 0);
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");

            return YapiTransport.Request<int>(ApiCommand.DeleteBanners, new CampaignBidsInfo { CampaignId = campaignId, BannerIds = bannerIds }) == 1;
        }

        public bool ModerateBanners(int campaignId, int[] bannerIds)
        {
            Contract.Requires<ArgumentException>(campaignId > 0);
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");

            return YapiTransport.Request<int>(ApiCommand.ModerateBanners, new CampaignBidsInfo { CampaignId = campaignId, BannerIds = bannerIds }) == 1;
        }

        public bool ResumeBanners(int campaignId, int[] bannerIds)
        {
            Contract.Requires<ArgumentException>(campaignId > 0);
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");

            return YapiTransport.Request<int>(ApiCommand.ResumeBanners, new CampaignBidsInfo { CampaignId = campaignId, BannerIds = bannerIds }) == 1;
        }

        public bool StopBanners(int campaignId, int[] bannerIds)
        {
            Contract.Requires<ArgumentException>(campaignId > 0);
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");

            return YapiTransport.Request<int>(ApiCommand.StopBanners, new CampaignBidsInfo { CampaignId = campaignId, BannerIds = bannerIds }) == 1;
        }

        public bool UnArchiveBanners(int campaignId, int[] bannerIds)
        {
            Contract.Requires<ArgumentException>(campaignId > 0);
            Contract.Requires<ArgumentException>(bannerIds != null && bannerIds.Any(), "bannerIds");

            return YapiTransport.Request<int>(ApiCommand.UnArchiveBanners, new CampaignBidsInfo { CampaignId = campaignId, BannerIds = bannerIds }) == 1;
        }

        #endregion

        #region Reporting

        public int CreateReport(NewReportInfo reportInfo)
        {
            Contract.Requires<ArgumentNullException>(reportInfo != null, "reportInfo");
            Contract.Requires<YapiException>(reportInfo.Limit.HasValue ^ reportInfo.Offset.HasValue, "Only one of \"Limit\" and \"Offset\" should be set");
            return YapiTransport.Request<int>(ApiCommand.CreateNewReport, reportInfo);
        }

        public ReportInfo[] ListReports()
        {
            return YapiTransport.Request<ReportInfo[]>(ApiCommand.GetReportList);
        }

        public GoalInfo[] GetStatGoals(int campId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(campId > 0, "campId");
            return YapiTransport.Request<GoalInfo[]>(ApiCommand.GetStatGoals, new StatGoalRequestInfo(campId));
        }

        public void DeleteReport(int reportId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(reportId > 0, "reportId");
            var result = YapiTransport.Request<int>(ApiCommand.DeleteReport, reportId);
            if (result != 1)
                throw new YapiServerException(string.Format("Плохой ответ. Должен вернуть: 1. Вернул: {0}", result));
        }

        #endregion

        #region Forecasts

        public int CreateForecast(string[] phrases, int[] geoIds = null, int[] categories = null)
        {
            Contract.Requires<ArgumentException>(phrases != null && phrases.Any(), "phrases");
            var reportInfo = new CreateForecastInfo
                                 {
                                     Categories = categories,
                                     GeoIds = geoIds,
                                     Phrases = phrases,
                                 };
            return YapiTransport.Request<int>(ApiCommand.CreateNewForecast, reportInfo);
        }

        public ForecastInfo GetForecast(int forecastId)
        {
            Contract.Requires<ArgumentOutOfRangeException>(forecastId > 0, "forecastId");
            return YapiTransport.Request<ForecastInfo>(ApiCommand.GetForecast, forecastId);
        }

        public ForecastStatus[] ListForecasts()
        {
            return YapiTransport.Request<ForecastStatus[]>(ApiCommand.GetForecastList);
        }

        public void DeleteForecast(int forecastReportId)
        {
            var result = YapiTransport.Request<int>(ApiCommand.DeleteForecastReport, forecastReportId);
            if (result != 1)
                throw new YapiServerException(string.Format("Плохой ответ. Должен вернуть: 1. Вернул: {0}", result));
        }

        #endregion
    }
}
