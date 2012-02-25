﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private JsonSerializerSettings JsonSettings { get; set; }

        public YapiService(YapiSettings settings)
        {
            Contract.Requires(settings != null);
            this.Setting = settings;
            this.JsonSettings =
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    Converters = { new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" } }
                };

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

        #region Отправка запросов к API Яндекса

        private string HttpRequest(string method, string parametersJson, bool sign = false)
        {
            Contract.Requires(!sign || !string.IsNullOrWhiteSpace(this.Setting.MasterToken), "Financial operations require MasterToken and Login to be set");
            var request = (HttpWebRequest)WebRequest.Create(this.Setting.ApiAddress);
            request.ClientCertificates.Add(new X509Certificate2(this.Setting.CertificatePath, this.Setting.CertificatePassword));
            request.Method = "POST";
            request.Proxy = null;
            request.ContentType = "application/json; charset=utf-8";

            var message = CreatePostMessage(method, parametersJson, sign);
            var data = Encoding.UTF8.GetBytes(message);

            request.ContentLength = data.Length;
            using (var requestStream = request.GetRequestStream())
                requestStream.Write(data, 0, data.Length);

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var responseStream = new StreamReader(stream))
                return responseStream.ReadToEnd();
        }

        private string CreatePostMessage(string method, string parametersJson, bool sign)
        {
            var json = new Dictionary<string, string>();
            Action<string, object> add = (key, value) => json[key] = value.ToString().StartsWith("\"") ? value.ToString() : "\"" + value + "\"";
            add("method", method);
            add("locale", JsonConvert.SerializeObject(this.Setting.Language, new StringEnumConverter()));

            if (sign)
            {
                var signature = new YandexSignature(this.Setting.MasterToken, method, this.Setting.Login);
                add("finance_token", signature.Token);
                add("operation_num", signature.OperationId);
                add("login", signature.Login);
            }

            if (parametersJson != null)
                if (parametersJson.StartsWith("[") || parametersJson.StartsWith("{"))
                    json["param"] = parametersJson;
                else
                    json["param"] = "\"" + parametersJson + "\"";

            var merged = json
                .Where(x => x.Value != null)
                .Select(x => string.Format("\"{0}\": {1}", x.Key, x.Value))
                .Merge(", ");

            return string.Format("{{{0}}}", merged);
        }

        private T Request<T>(ApiCommand method, object requestData = null, bool sign = false)
        {
            if (!Enum.IsDefined(typeof(ApiCommand), method))
                throw new InvalidEnumArgumentException("method", (int)method, typeof(ApiCommand));

            var json = HttpRequest(method.ToString(), requestData == null ? null : SerializeToJson(requestData), sign);
            var error = JsonConvert.DeserializeObject<YandexErrorInfo>(json, JsonSettings);
            if (error != null && error.Code != 0)
                throw new ApplicationException(error.Error);
            var response = JsonConvert.DeserializeObject<YapiResponse<T>>(json, JsonSettings);
            return response.Data;
        }

        private string SerializeToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, JsonSettings);
        }

        #endregion

        #region PingApi

        public int PingApi()
        {
            return Request<int>(ApiCommand.PingAPI);
        }

        public bool TestApiConnection()
        {
            return PingApi() == 1;
        }

        #endregion

        #region Clients

        public List<ShortClientInfo> GetClientLogins()
        {
            return Request<List<ShortClientInfo>>(ApiCommand.GetClientsList);
        }

        public ClientUnitInfo[] GetClientsUnits(params string[] logins)
        {
            Contract.Requires(logins != null && logins.Any());
            return Request<ClientUnitInfo[]>(ApiCommand.GetClientsUnits, logins);
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
            return Request<List<ShortCampaignInfo>>(ApiCommand.GetCampaignsList, logins);
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
            Request<int>(ApiCommand.TransferMoney, request, true);
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
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            return Request<int>(ApiCommand.ArchiveBanners, new CampaignBidsInfo { CampaignId = campaignId, BannerIds = bannerIds }) == 1;
        }

        public bool DeleteBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            return Request<int>(ApiCommand.DeleteBanners, new CampaignBidsInfo { CampaignId = campaignId, BannerIds = bannerIds }) == 1;
        }

        public bool ModerateBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            return Request<int>(ApiCommand.ModerateBanners, new CampaignBidsInfo { CampaignId = campaignId, BannerIds = bannerIds }) == 1;
        }

        public bool ResumeBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            return Request<int>(ApiCommand.ResumeBanners, new CampaignBidsInfo { CampaignId = campaignId, BannerIds = bannerIds }) == 1;
        }

        public bool StopBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            return Request<int>(ApiCommand.StopBanners, new CampaignBidsInfo { CampaignId = campaignId, BannerIds = bannerIds }) == 1;
        }

        public bool UnArchiveBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            return Request<int>(ApiCommand.UnArchiveBanners, new CampaignBidsInfo { CampaignId = campaignId, BannerIds = bannerIds }) == 1;
        }

        #endregion

        #region Reporting

        public int CreateReport(NewReportInfo reportInfo)
        {
            Contract.Requires(reportInfo != null && reportInfo.Limit.HasValue ^ reportInfo.Offset.HasValue);
            return Request<int>(ApiCommand.CreateNewReport, reportInfo);
        }

        public ReportInfo[] ListReports()
        {
            return Request<ReportInfo[]>(ApiCommand.GetReportList);
        }

        public GoalInfo[] GetStatGoals(int campId)
        {
            return Request<GoalInfo[]>(ApiCommand.GetStatGoals, new StatGoalRequestInfo(campId));
        }

        public void DeleteReport(int reportId)
        {
            var result = Request<int>(ApiCommand.DeleteReport, reportId);
            if (result != 1)
                throw new ApplicationException("Плохой ответ. Должен вернуть: 1. Вернул: " + result);
        }

        #endregion

        #region Forecasts

        public int CreateForecast(IEnumerable<string> phrases, int[] geoIds = null, int[] categories = null)
        {
            Contract.Requires(phrases != null);
            var reportInfo = new CreateForecastInfo
                                 {
                                     Categories = categories,
                                     GeoIds = geoIds,
                                     Phrases = phrases.ToArray(),
                                 };
            return Request<int>(ApiCommand.CreateNewForecast, reportInfo);
        }

        public ForecastInfo GetForecast(int forecastId)
        {
            Contract.Requires(forecastId > 0);
            return Request<ForecastInfo>(ApiCommand.GetForecast, forecastId);
        }

        public ForecastStatus[] ListForecasts()
        {
            return Request<ForecastStatus[]>(ApiCommand.GetForecastList);
        }

        public void DeleteForecast(int forecastReportId)
        {
            var result = Request<int>(ApiCommand.DeleteForecastReport, forecastReportId);
            if (result != 1)
                throw new ApplicationException("Плохой ответ. Должен вернуть: 1. Вернул: " + result);
        }

        #endregion
    }
}
