using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Yandex.Direct.Authentication;
using Yandex.Direct.Configuration;
using Yandex.Direct.Connectivity;

namespace Yandex.Direct
{
    public partial class YandexDirectService
    {
        static class ApiMethod
        {
            // ReSharper disable InconsistentNaming

            // Finances

            public const string CreateInvoice = "CreateInvoice";
            public const string GetCreditLimits = "GetCreditLimits";
            public const string PayCampaigns = "PayCampaigns";
            public const string TransferMoney = "TransferMoney";

            // Campaign statistics

            public const string GetSummaryStat = "GetSummaryStat";
            public const string CreateNewReport = "CreateNewReport";
            public const string DeleteReport = "DeleteReport";
            public const string GetReportList = "GetReportList";
            public const string GetStatGoals = "GetStatGoals";

            // Keywords

            public const string CreateNewWordstatReport = "CreateNewWordstatReport";
            public const string DeleteWordstatReport = "DeleteWordstatReport";
            public const string GetKeywordsSuggestion = "GetKeywordsSuggestion";
            public const string GetWordstatReport = "GetWordstatReport";
            public const string GetWordstatReportList = "GetWordstatReportList";

            // Budget forecasting

            public const string CreateNewForecast = "CreateNewForecast";
            public const string DeleteForecastReport = "DeleteForecastReport";
            public const string GetForecast = "GetForecast";
            public const string GetForecastList = "GetForecastList";

            // Campaigns

            public const string CreateOrUpdateCampaign = "CreateOrUpdateCampaign";
            public const string GetBalance = "GetBalance";
            public const string GetCampaignParams = "GetCampaignParams";
            public const string GetCampaignsList = "GetCampaignsList";
            public const string GetCampaignsListFilter = "GetCampaignsListFilter";
            public const string GetCampaignsParams = "GetCampaignsParams";

            // Campaign state

            public const string ArchiveCampaign = "ArchiveCampaign";
            public const string DeleteCampaign = "DeleteCampaign";
            public const string ResumeCampaign = "ResumeCampaign";
            public const string StopCampaign = "StopCampaign";
            public const string UnArchiveCampaign = "UnArchiveCampaign";

            // Ads and phrases

            public const string CreateOrUpdateBanners = "CreateOrUpdateBanners";
            public const string GetBanners = "GetBanners";
            public const string GetBannerPhrases = "GetBannerPhrases";
            public const string GetBannerPhrasesFilter = "GetBannerPhrasesFilter";

            // Ad status

            public const string ArchiveBanners = "ArchiveBanners";
            public const string DeleteBanners = "DeleteBanners";
            public const string ModerateBanners = "ModerateBanners";
            public const string ResumeBanners = "ResumeBanners";
            public const string StopBanners = "StopBanners";
            public const string UnArchiveBanners = "UnArchiveBanners";

            // Cost Per Click

            public const string SetAutoPrice = "SetAutoPrice";
            public const string UpdatePrices = "UpdatePrices";

            // Clients

            public const string CreateNewSubclient = "CreateNewSubclient";
            public const string GetClientInfo = "GetClientInfo";
            public const string GetClientsList = "GetClientsList";
            public const string GetClientsUnits = "GetClientsUnits";
            public const string GetSubClients = "GetSubClients";
            public const string UpdateClientInfo = "UpdateClientInfo";

            // Yandex.Catalog and regions

            public const string GetRegions = "GetRegions";
            public const string GetRubrics = "GetRubrics";

            // Other methods

            public const string GetAvailableVersions = "GetAvailableVersions";
            public const string GetChanges = "GetChanges";
            public const string GetTimeZones = "GetTimeZones";
            public const string GetVersion = "GetVersion";
            public const string PingAPI = "PingAPI";

            // ReSharper restore InconsistentNaming
        }

        protected IYandexApiClient YandexApiClient { get; set; }

        public YandexDirectConfiguration Configuration { get; private set; }

        public YandexDirectService(YandexDirectConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            Configuration = configuration;
            YandexApiClient = new JsonYandexApiClient(configuration);

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, errors) => true;
        }

        public YandexDirectService()
            : this(new YandexDirectConfiguration())
        {
        }

        public YandexDirectService(IYandexApiAuthProvider authProvider)
            : this(new YandexDirectConfiguration(authProvider))
        {
        }

        public YandexDirectService(IYandexApiAuthProvider authProvider, YandexApiLanguage language)
            : this(new YandexDirectConfiguration(authProvider, language))
        {
        }

        public int PingApi()
        {
            return YandexApiClient.Invoke<int>(ApiMethod.PingAPI);
        }

        public void TestApiConnection()
        {
            if (PingApi() != 1)
                throw new YapiServerException("Yandex.Direct API ping attempt failed.");
        }
    }
}
